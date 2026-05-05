USE [TMIS_Development]
GO

IF OBJECT_ID('dbo.POS_DebtorMenuItemProducts_insert', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_DebtorMenuItemProducts_insert;
GO

CREATE PROCEDURE dbo.POS_DebtorMenuItemProducts_insert
    @FK_DebtorMenuItemID INT = NULL,
    @FK_ProductID INT,
    @IsActive BIT,
    @DateCreated DATETIME,
    @FK_CreatedUserID INT,
    @DateUpdated DATETIME,
    @FK_UpdatedUserID INT,
    @DisplayOrder INT
AS
BEGIN
    DECLARE @Inserted TABLE (MenuItemProductID INT);

    INSERT INTO POS_DebtorMenuItemProducts (FK_DebtorMenuItemID, FK_ProductID, IsActive, DateCreated, FK_CreatedUserID, DateUpdated, FK_UpdatedUserID, DisplayOrder)
    OUTPUT INSERTED.MenuItemProductID INTO @Inserted
    VALUES (@FK_DebtorMenuItemID, @FK_ProductID, @IsActive, @DateCreated, @FK_CreatedUserID, @DateUpdated, @FK_UpdatedUserID, @DisplayOrder);

    SELECT *
    FROM POS_DebtorMenuItemProducts
    WHERE MenuItemProductID = 
    (
        SELECT TOP 1 MenuItemProductID
        FROM @Inserted
    );
END
GO