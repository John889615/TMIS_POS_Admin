USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_MenuItemProducts_update', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_MenuItemProducts_update;
GO

CREATE PROCEDURE dbo.POS_MenuItemProducts_update
    @MenuItemProductID INT,
    @FK_MenuItemID INT = NULL,
    @FK_ProductID INT,
    @DateCreated DATETIME,
    @FK_CreatedUserID INT,
    @DateUpdated DATETIME = NULL,
    @FK_UpdatedUserID INT,
    @DisplayOrder INT
AS
BEGIN
    UPDATE POS_MenuItemProducts
    SET     FK_MenuItemID = @FK_MenuItemID,
    FK_ProductID = @FK_ProductID,
    FK_CreatedUserID = @FK_CreatedUserID,
    DateUpdated = @DateUpdated,
    FK_UpdatedUserID = @FK_UpdatedUserID,
    DisplayOrder = @DisplayOrder
    WHERE MenuItemProductID = @MenuItemProductID;

    SELECT *
    FROM POS_MenuItemProducts
    WHERE MenuItemProductID = @MenuItemProductID;
END
GO