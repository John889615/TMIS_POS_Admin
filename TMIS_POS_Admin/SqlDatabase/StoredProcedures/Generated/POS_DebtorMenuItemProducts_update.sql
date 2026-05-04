USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_DebtorMenuItemProducts_update', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_DebtorMenuItemProducts_update;
GO

CREATE PROCEDURE dbo.POS_DebtorMenuItemProducts_update
    @MenuItemProductID INT,
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
    UPDATE POS_DebtorMenuItemProducts
    SET     FK_DebtorMenuItemID = @FK_DebtorMenuItemID,
    FK_ProductID = @FK_ProductID,
    IsActive = @IsActive,
    FK_CreatedUserID = @FK_CreatedUserID,
    DateUpdated = @DateUpdated,
    FK_UpdatedUserID = @FK_UpdatedUserID,
    DisplayOrder = @DisplayOrder
    WHERE MenuItemProductID = @MenuItemProductID;

    SELECT *
    FROM POS_DebtorMenuItemProducts
    WHERE MenuItemProductID = @MenuItemProductID;
END
GO