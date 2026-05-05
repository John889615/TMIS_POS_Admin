USE [TMIS_Development]
GO

IF OBJECT_ID('dbo.POS_MenuItems_insert', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_MenuItems_insert;
GO

CREATE PROCEDURE dbo.POS_MenuItems_insert
    @FK_MenuID INT = NULL,
    @Item VARCHAR(50),
    @Description VARCHAR(255) = NULL,
    @FK_MenuItemID INT = NULL,
    @DateCreated DATETIME,
    @FK_CreatedUserID INT,
    @DateUpdated DATETIME,
    @FK_UpdatedUserID INT
AS
BEGIN
    DECLARE @Inserted TABLE (MenuItemID INT);

    INSERT INTO POS_MenuItems (FK_MenuID, Item, Description, FK_MenuItemID, DateCreated, FK_CreatedUserID, DateUpdated, FK_UpdatedUserID)
    OUTPUT INSERTED.MenuItemID INTO @Inserted
    VALUES (@FK_MenuID, @Item, @Description, @FK_MenuItemID, @DateCreated, @FK_CreatedUserID, @DateUpdated, @FK_UpdatedUserID);

    SELECT *
    FROM POS_MenuItems
    WHERE MenuItemID = 
    (
        SELECT TOP 1 MenuItemID
        FROM @Inserted
    );
END
GO