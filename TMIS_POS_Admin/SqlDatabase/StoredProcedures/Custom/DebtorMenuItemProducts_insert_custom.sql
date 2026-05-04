USE [TMIS_Development]
GO

IF OBJECT_ID('dbo.DebtorMenuItemProducts_insert_custom', 'P') IS NOT NULL
    DROP PROCEDURE dbo.DebtorMenuItemProducts_insert_custom;
GO

CREATE PROCEDURE dbo.DebtorMenuItemProducts_insert_custom
    @POS_DebtorMenuItemProductID INT,
	@FK_DebtorMenuItemID INT = NULL,
    @FK_ProductID INT,
    @FK_DebtorProductID INT = NULL,
    @IsActive BIT,
    @DateCreated DATETIME,
    @FK_CreatedUserID INT,
    @DateUpdated DATETIME,
    @FK_UpdatedUserID INT
AS
BEGIN
	SET IDENTITY_INSERT POS_DebtorMenuItemProducts ON;

    INSERT INTO POS_DebtorMenuItemProducts (POS_MenuItemProductID, FK_DebtorMenuItemID, FK_ProductID, FK_DebtorProductID, IsActive, DateCreated, FK_CreatedUserID, DateUpdated, FK_UpdatedUserID)
    VALUES (@POS_DebtorMenuItemProductID, @FK_DebtorMenuItemID, @FK_ProductID, @FK_DebtorProductID, @IsActive, @DateCreated, @FK_CreatedUserID, @DateUpdated, @FK_UpdatedUserID)
    
    SET IDENTITY_INSERT POS_DebtorMenuItemProducts OFF;

	SELECT * FROM POS_DebtorMenuItemProducts
    WHERE POS_MenuItemProductID = @POS_DebtorMenuItemProductID;
END
GO