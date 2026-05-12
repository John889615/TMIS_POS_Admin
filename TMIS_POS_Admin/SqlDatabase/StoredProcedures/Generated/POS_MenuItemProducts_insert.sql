USE [TMIS_Development]
GO

IF OBJECT_ID('dbo.POS_MenuItemProducts_insert', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_MenuItemProducts_insert;
GO

CREATE PROCEDURE dbo.POS_MenuItemProducts_insert
    @FK_MenuItemID INT = NULL,
    @FK_ProductID INT,
    @DateCreated DATETIME,
    @FK_CreatedUserID INT,
    @DateUpdated DATETIME = NULL,
    @FK_UpdatedUserID INT,
    @DisplayOrder INT
AS
BEGIN
    DECLARE @Inserted TABLE (MenuItemProductID INT);

    INSERT INTO POS_MenuItemProducts (FK_MenuItemID, FK_ProductID, DateCreated, FK_CreatedUserID, DateUpdated, FK_UpdatedUserID, DisplayOrder)
    OUTPUT INSERTED.MenuItemProductID INTO @Inserted
    VALUES (@FK_MenuItemID, @FK_ProductID, @DateCreated, @FK_CreatedUserID, @DateUpdated, @FK_UpdatedUserID, @DisplayOrder);

    SELECT *
    FROM POS_MenuItemProducts
    WHERE MenuItemProductID = 
    (
        SELECT TOP 1 MenuItemProductID
        FROM @Inserted
    );
END
GO