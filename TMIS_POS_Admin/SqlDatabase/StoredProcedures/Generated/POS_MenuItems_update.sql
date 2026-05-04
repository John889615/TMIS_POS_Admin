USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_MenuItems_update', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_MenuItems_update;
GO

CREATE PROCEDURE dbo.POS_MenuItems_update
    @MenuItemID INT,
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
    UPDATE POS_MenuItems
    SET     FK_MenuID = @FK_MenuID,
    Item = @Item,
    Description = @Description,
    FK_MenuItemID = @FK_MenuItemID,
    FK_CreatedUserID = @FK_CreatedUserID,
    DateUpdated = @DateUpdated,
    FK_UpdatedUserID = @FK_UpdatedUserID
    WHERE MenuItemID = @MenuItemID;

    SELECT *
    FROM POS_MenuItems
    WHERE MenuItemID = @MenuItemID;
END
GO