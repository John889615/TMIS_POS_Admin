USE [TMIS_Development]
GO

IF OBJECT_ID('dbo.CopyMenuToDebtor', 'P') IS NOT NULL
    DROP PROCEDURE dbo.CopyMenuToDebtor;
GO

-- EXEC dbo.CopyMenuToDebtor 3, 26, 1426, 1, 1

CREATE PROCEDURE dbo.CopyMenuToDebtor
    @SourceMenuID INT,
    @TargetDebtorID INT,
    @TargetCostCenterID INT = NULL,
    @UserID INT,
    @Override BIT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    DECLARE @NewDebtorMenuID INT;
    DECLARE @ExistingCount INT;

    BEGIN TRY
        BEGIN TRANSACTION;

        SELECT @ExistingCount = COUNT(1)
        FROM POS_DebtorMenus dm
        WHERE dm.FK_MenuID = @SourceMenuID
          AND dm.FK_LocationID = @TargetDebtorID
          AND ISNULL(dm.FK_CostCenterID, 0) = ISNULL(@TargetCostCenterID, 0);

        -- If one or more already exist and override is OFF, do nothing
        IF ISNULL(@ExistingCount, 0) > 0 AND ISNULL(@Override, 0) = 0
        BEGIN
            PRINT 'Menu not copied, already exists for this Menu+Location+CostCenter.';
            ROLLBACK TRANSACTION;
            RETURN;
        END

        -- If duplicates or existing rows are found and override is ON,
        -- delete ALL matching rows for this Menu + Location + CostCenter
        IF ISNULL(@ExistingCount, 0) > 0 AND ISNULL(@Override, 0) = 1
        BEGIN
            -- 1. Delete debtor menu item products first
            DELETE dmp
            FROM POS_DebtorMenuItemProducts dmp
            INNER JOIN POS_DebtorMenuItems dmi
                ON dmp.FK_DebtorMenuItemID = dmi.DebtorMenuItemID
            INNER JOIN POS_DebtorMenus dm
                ON dmi.FK_DebtorMenuID = dm.DebtorMenuID
            WHERE dm.FK_MenuID = @SourceMenuID
              AND dm.FK_LocationID = @TargetDebtorID
              AND ISNULL(dm.FK_CostCenterID, 0) = ISNULL(@TargetCostCenterID, 0);

            -- 2. Delete debtor menu items
            DELETE dmi
            FROM POS_DebtorMenuItems dmi
            INNER JOIN POS_DebtorMenus dm
                ON dmi.FK_DebtorMenuID = dm.DebtorMenuID
            WHERE dm.FK_MenuID = @SourceMenuID
              AND dm.FK_LocationID = @TargetDebtorID
              AND ISNULL(dm.FK_CostCenterID, 0) = ISNULL(@TargetCostCenterID, 0);

            -- 3. Delete debtor menu printers
            DELETE dmpn
            FROM POS_DebtorMenuPrinters dmpn
            INNER JOIN POS_DebtorMenus dm
                ON dmpn.FK_DebtorMenuID = dm.DebtorMenuID
            WHERE dm.FK_MenuID = @SourceMenuID
              AND dm.FK_LocationID = @TargetDebtorID
              AND ISNULL(dm.FK_CostCenterID, 0) = ISNULL(@TargetCostCenterID, 0);

            -- 4. Delete debtor menus
            DELETE dm
            FROM POS_DebtorMenus dm
            WHERE dm.FK_MenuID = @SourceMenuID
              AND dm.FK_LocationID = @TargetDebtorID
              AND ISNULL(dm.FK_CostCenterID, 0) = ISNULL(@TargetCostCenterID, 0);
        END

        CREATE TABLE #TempMenuIDs
        (
            ParentMenuItemID INT NOT NULL,
            NewMenuItemID INT NOT NULL
        );

        -- STEP 1: Clone POS_Menus into POS_DebtorMenus
        INSERT INTO POS_DebtorMenus
        (
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

        -- Insert missing debtor products needed by this menu
        INSERT INTO POS_DebtorProducts
        (
            FK_ProductID,
            FK_LocationID,
            FK_SellUnitID,
            CostPrice,
            QuantityOnHand,
            IsAvailable,
            IsActive,
            FK_CreatedUserID,
            FK_UpdatedUserID,
            DateCreated,
            DateUpdated
        )
        SELECT
            p.ProductID,
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
        WHERE p.ProductID IN
        (
            SELECT DISTINCT mip.FK_ProductID
            FROM POS_Menus m
            INNER JOIN POS_MenuItems mi
                ON m.MenuID = mi.FK_MenuID
            INNER JOIN POS_MenuItemProducts mip
                ON mi.MenuItemID = mip.FK_MenuItemID
            LEFT JOIN POS_DebtorProducts dp
                ON dp.FK_LocationID = @TargetDebtorID
               AND dp.FK_ProductID = mip.FK_ProductID
            WHERE m.MenuID = @SourceMenuID
              AND dp.FK_ProductID IS NULL
        );

        -- Insert missing cost center products
        INSERT INTO POS_CostCenterProducts
        (
            FK_ProductID,
            FK_CostCenterID,
            FK_TaxTypeID,
            [Value],
            Vat,
            ItemPrice,
            FK_SellUnitID,
            QuantityOnHand,
            IsAvailable,
            IsActive,
            FK_CreatedUserID,
            FK_UpdatedUserID,
            DateCreated,
            DateUpdated
        )
        SELECT
            p.ProductID,
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
        WHERE p.ProductID IN
        (
            SELECT DISTINCT mip.FK_ProductID
            FROM POS_Menus m
            INNER JOIN POS_MenuItems mi
                ON m.MenuID = mi.FK_MenuID
            INNER JOIN POS_MenuItemProducts mip
                ON mi.MenuItemID = mip.FK_MenuItemID
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
            (
                FK_DebtorMenuID,
                Item,
                [Description],
                FK_MenuItemID,
                DateCreated,
                FK_CreatedUserID,
                DateUpdated,
                FK_UpdatedUserID
            )
            SELECT
                @NewDebtorMenuID,
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

            INSERT INTO #TempMenuIDs
            (
                ParentMenuItemID,
                NewMenuItemID
            )
            VALUES
            (
                @CurrentMenuItemID,
                @NewPosMenuItemID
            );

            FETCH NEXT FROM db_cursor INTO @CurrentMenuItemID;
        END

        CLOSE db_cursor;
        DEALLOCATE db_cursor;

        -- Re-link FK_MenuItemID based on parent-child relationships in POS_MenuItems
        UPDATE dmi
        SET dmi.FK_MenuItemID = t3.FK_MenuItemID
        FROM POS_DebtorMenuItems dmi
        INNER JOIN
        (
            SELECT
                t1.NewMenuItemID AS POS_DebtorMenuItemID,
                t2.NewMenuItemID AS FK_MenuItemID
            FROM POS_MenuItems mi
            INNER JOIN #TempMenuIDs t1
                ON mi.MenuItemID = t1.ParentMenuItemID
            INNER JOIN #TempMenuIDs t2
                ON mi.FK_MenuItemID = t2.ParentMenuItemID
            WHERE mi.MenuItemID IN (SELECT ParentMenuItemID FROM #TempMenuIDs)
              AND mi.FK_MenuItemID IS NOT NULL
        ) t3
            ON dmi.DebtorMenuItemID = t3.POS_DebtorMenuItemID;

        -- Copy item products
        INSERT INTO POS_DebtorMenuItemProducts
        (
            FK_DebtorMenuItemID,
            FK_ProductID,
            IsActive,
            DateCreated,
            FK_CreatedUserID,
            DateUpdated,
            FK_UpdatedUserID
        )
        SELECT
            t.NewMenuItemID,
            mip.FK_ProductID,
            1,
            GETDATE(),
            @UserID,
            GETDATE(),
            @UserID
        FROM POS_MenuItemProducts mip
        INNER JOIN #TempMenuIDs t
            ON mip.FK_MenuItemID = t.ParentMenuItemID;

        INSERT INTO POS_Images
        (
            FK_ImageCategoryID,
            FK_ItemID,
            FileSystemPath,
            RelativePath,
            ImageName,
            FileExtension,
            ImageUrl,
            LocalUrl,
            DateCreated,
            DateUpdated
        )
        SELECT
            FK_ImageCategoryID,
            @NewDebtorMenuID,
            FileSystemPath,
            'debtor_menus',
            ImageName,
            FileExtension,
            ImageUrl,
            LocalUrl,
            GETDATE(),
            GETDATE()
        FROM POS_Images
        WHERE FK_ItemID = @SourceMenuID
          AND RelativePath = 'menus';

        DROP TABLE #TempMenuIDs;

        COMMIT TRANSACTION;

        PRINT 'Menu copied successfully to Debtor ID = ' + CAST(@TargetDebtorID AS VARCHAR);

        SELECT *
        FROM POS_DebtorMenus
        WHERE DebtorMenuID = @NewDebtorMenuID;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        THROW;
    END CATCH
END;
GO