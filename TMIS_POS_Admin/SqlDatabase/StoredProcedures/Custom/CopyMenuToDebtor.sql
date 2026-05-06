USE [TMIS_Development]
GO

IF OBJECT_ID('CopyMenuToDebtor', 'P') IS NOT NULL
    DROP PROCEDURE CopyMenuToDebtor
GO

-- EXEC CopyMenuToDebtor 1, 5, 1, 1, 1

CREATE PROCEDURE [dbo].[CopyMenuToDebtor]
    @SourceMenuID INT,
    @TargetDebtorID INT,
    @TargetCostCenterID INT = NULL,
    @UserID INT,
    @Override BIT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @NewDebtorMenuID INT;
    DECLARE @ExistingDebtorMenuID INT;

    -- Find existing menu for this exact combo: Menu + Location + CostCenter
    SELECT TOP 1 @ExistingDebtorMenuID = dm.DebtorMenuID
    FROM POS_DebtorMenus dm
    WHERE dm.FK_MenuID = @SourceMenuID
      AND dm.FK_LocationID = @TargetDebtorID
      AND ISNULL(dm.FK_CostCenterID, 0) = ISNULL(@TargetCostCenterID, 0);

    -- If it exists and override is off, bail BEFORE touching anything.
    -- (Old code deleted first then checked override - that wiped data on cancel.)
    IF @ExistingDebtorMenuID IS NOT NULL AND ISNULL(@Override, 0) = 0
    BEGIN
        PRINT 'Menu not copied, already exists for this Menu+Location+CostCenter.';
        RETURN;
    END

    CREATE TABLE #TempMenuIDs
    (
        ParentMenuItemID INT NOT NULL,
        NewMenuItemID INT NOT NULL
    );

    IF @ExistingDebtorMenuID IS NOT NULL
    BEGIN
        -- Override mode: replace the children but keep the POS_DebtorMenus
        -- row stable. Reason: its POS_DebtorMenuID flows to the FoH as
        -- dbo.Menus.MenuID, and TabLines.FK_MenuID will block any delete
        -- of that menu on the FoH side. Order is leaf-first to avoid
        -- orphaning POS_DebtorMenuItemProductPrinters rows.
        DELETE FROM POS_DebtorMenuItemProductPrinters
         WHERE FK_MenuItemProductID IN (
               SELECT dmp.MenuItemProductID
                 FROM POS_DebtorMenuItemProducts dmp
                 INNER JOIN POS_DebtorMenuItems dmi
                    ON dmp.FK_DebtorMenuItemID = dmi.DebtorMenuItemID
                WHERE dmi.FK_DebtorMenuID = @ExistingDebtorMenuID);

        DELETE dmp
        FROM POS_DebtorMenuItemProducts dmp
        INNER JOIN POS_DebtorMenuItems dmi
            ON dmp.FK_DebtorMenuItemID = dmi.DebtorMenuItemID
        WHERE dmi.FK_DebtorMenuID = @ExistingDebtorMenuID;

        DELETE FROM POS_DebtorMenuItems
        WHERE FK_DebtorMenuID = @ExistingDebtorMenuID;

        UPDATE POS_DebtorMenus
           SET FK_LocationID   = @TargetDebtorID,
               FK_CostCenterID = @TargetCostCenterID,
               MenuName        = (SELECT MenuName FROM POS_Menus WHERE MenuID = @SourceMenuID),
               IsActive        = 1,
               DateUpdated     = GETDATE()
         WHERE DebtorMenuID = @ExistingDebtorMenuID;

        SET @NewDebtorMenuID = @ExistingDebtorMenuID;
    END
    ELSE
    BEGIN
        -- STEP 1: Clone POS_Menus into POS_DebtorMenus
        INSERT INTO POS_DebtorMenus (
            FK_LocationID,
            FK_CostCenterID,
            FK_MenuID,
            MenuName,
            ValidFrom,
            ValidTo,
            IsActive,
            DateCreated,
            DateUpdated
        )
        SELECT
            @TargetDebtorID,
            @TargetCostCenterID,
            MenuID,
            MenuName,
            GETDATE(),
            NULL,
            1,
            GETDATE(),
            GETDATE()
        FROM POS_Menus
        WHERE MenuID = @SourceMenuID;

        SET @NewDebtorMenuID = SCOPE_IDENTITY();
    END

    -- Insert missing debtor products needed by this menu
    INSERT INTO POS_DebtorProducts
        (FK_ProductID, FK_LocationID, FK_SellUnitID, CostPrice, QuantityOnHand, IsAvailable, IsActive, FK_CreatedUserID, FK_UpdatedUserID, DateCreated, DateUpdated)
    SELECT p.ProductID,
           @TargetDebtorID,
           p.FK_DefaultUnitID,
           0,
           0,
           1,
           1,
           @UserID,
           @UserID,
           GETDATE(),
           GETDATE()
    FROM POS_Products p
    WHERE p.ProductID IN (
        SELECT DISTINCT mip.FK_ProductID
        FROM POS_Menus m
        INNER JOIN POS_MenuItems mi ON m.MenuID = mi.FK_MenuID
        INNER JOIN POS_MenuItemProducts mip ON mi.MenuItemID = mip.FK_MenuItemID
        LEFT JOIN POS_DebtorProducts dp
            ON dp.FK_LocationID = @TargetDebtorID
           AND dp.FK_ProductID = mip.FK_ProductID
        WHERE m.MenuID = @SourceMenuID
          AND dp.FK_ProductID IS NULL
    );

    -- Insert missing costcenter products (only if TargetCostCenterID is provided)
    INSERT INTO POS_CostCenterProducts
        (FK_ProductID, FK_CostCenterID, FK_TaxTypeID, [Value], Vat, ItemPrice, FK_SellUnitID, QuantityOnHand, IsAvailable, IsActive, FK_CreatedUserID, FK_UpdatedUserID, DateCreated, DateUpdated)
    SELECT p.ProductID,
           @TargetCostCenterID,
           1,
           0,
           0,
           0,
           p.FK_DefaultUnitID,
           0,
           1,
           1,
           @UserID,
           @UserID,
           GETDATE(),
           GETDATE()
    FROM POS_Products p
    WHERE p.ProductID IN (
        SELECT DISTINCT mip.FK_ProductID
        FROM POS_Menus m
        INNER JOIN POS_MenuItems mi ON m.MenuID = mi.FK_MenuID
        INNER JOIN POS_MenuItemProducts mip ON mi.MenuItemID = mip.FK_MenuItemID
        LEFT JOIN POS_CostCenterProducts ccp
            ON ccp.FK_CostCenterID = @TargetCostCenterID
           AND ccp.FK_ProductID = mip.FK_ProductID
        WHERE m.MenuID = @SourceMenuID
          AND ccp.FK_ProductID IS NULL
    )
    AND ISNULL(@TargetCostCenterID, 0) <> 0;

    DECLARE @CurrentMenuItemID INT;
    DECLARE @NewPosMenuItemID INT;

    DECLARE db_cursor CURSOR FOR 
        SELECT MenuItemID
        FROM POS_MenuItems
        WHERE FK_MenuID = @SourceMenuID;

    OPEN db_cursor;
    FETCH NEXT FROM db_cursor INTO @CurrentMenuItemID;

    WHILE @@FETCH_STATUS = 0
    BEGIN
        INSERT INTO POS_DebtorMenuItems
            (FK_DebtorMenuID, Item, [Description], FK_MenuItemID, DateCreated, FK_CreatedUserID, DateUpdated, FK_UpdatedUserID)
        SELECT @NewDebtorMenuID,
               Item,
               [Description],
               NULL,
               GETDATE(),
               @UserID,
               GETDATE(),
               @UserID
        FROM POS_MenuItems
        WHERE MenuItemID = @CurrentMenuItemID;

        SET @NewPosMenuItemID = SCOPE_IDENTITY();

        INSERT INTO #TempMenuIDs (ParentMenuItemID, NewMenuItemID)
        VALUES (@CurrentMenuItemID, @NewPosMenuItemID);

        FETCH NEXT FROM db_cursor INTO @CurrentMenuItemID;
    END

    CLOSE db_cursor;
    DEALLOCATE db_cursor;

    -- Re-link FK_MenuItemID based on parent-child relationships in POS_MenuItems
    UPDATE dmi
    SET dmi.FK_MenuItemID = t3.FK_MenuItemID
    FROM POS_DebtorMenuItems dmi
    INNER JOIN (
        SELECT t1.NewMenuItemID AS POS_DebtorMenuItemID,
               t2.NewMenuItemID AS FK_MenuItemID
        FROM POS_MenuItems mi
        INNER JOIN #TempMenuIDs t1 ON mi.MenuItemID = t1.ParentMenuItemID
        INNER JOIN #TempMenuIDs t2 ON mi.FK_MenuItemID = t2.ParentMenuItemID
        WHERE mi.MenuItemID IN (SELECT ParentMenuItemID FROM #TempMenuIDs)
          AND mi.FK_MenuItemID IS NOT NULL
    ) t3 ON dmi.DebtorMenuItemID = t3.POS_DebtorMenuItemID;

    -- Copy item products
    INSERT INTO POS_DebtorMenuItemProducts
        (FK_DebtorMenuItemID, FK_ProductID, IsActive, DateCreated, FK_CreatedUserID, DateUpdated, FK_UpdatedUserID)
    SELECT t.NewMenuItemID,
           mip.FK_ProductID,
           1,
           GETDATE(),
           @UserID,
           GETDATE(),
           @UserID
    FROM POS_MenuItemProducts mip
    INNER JOIN #TempMenuIDs t ON mip.FK_MenuItemID = t.ParentMenuItemID;

    PRINT 'Menu copied successfully to Debtor ID = ' + CAST(@TargetDebtorID AS VARCHAR);

    DROP TABLE #TempMenuIDs;

    SELECT *
    FROM POS_DebtorMenus
    WHERE DebtorMenuID = @NewDebtorMenuID;
END;
GO
