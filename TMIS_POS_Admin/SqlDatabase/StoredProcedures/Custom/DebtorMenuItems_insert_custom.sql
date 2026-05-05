USE [TMIS_Development]
GO

IF OBJECT_ID('dbo.DebtorMenuItems_insert_custom', 'P') IS NOT NULL
    DROP PROCEDURE dbo.DebtorMenuItems_insert_custom;
GO

CREATE PROCEDURE dbo.DebtorMenuItems_insert_custom
    @POS_DebtorMenuItemID INT,
	@FK_DebtorMenuID INT = NULL,
    @Item VARCHAR(50),
    @Description VARCHAR(255) = NULL,
    @FK_MenuItemID INT = NULL,
    @DateCreated DATETIME,
    @FK_CreatedUserID INT,
    @DateUpdated DATETIME,
    @FK_UpdatedUserID INT
AS
BEGIN
	SET IDENTITY_INSERT POS_DebtorMenuItems ON;

    INSERT INTO POS_DebtorMenuItems (POS_DebtorMenuItemID, FK_DebtorMenuID, Item, Description, FK_MenuItemID, DateCreated, FK_CreatedUserID, DateUpdated, FK_UpdatedUserID)
    VALUES (@POS_DebtorMenuItemID, @FK_DebtorMenuID, @Item, @Description, @FK_MenuItemID, @DateCreated, @FK_CreatedUserID, @DateUpdated, @FK_UpdatedUserID)
    
    SET IDENTITY_INSERT POS_DebtorMenuItems OFF;

	SELECT * FROM POS_DebtorMenuItems
    WHERE POS_DebtorMenuItemID = @POS_DebtorMenuItemID;
END
GO