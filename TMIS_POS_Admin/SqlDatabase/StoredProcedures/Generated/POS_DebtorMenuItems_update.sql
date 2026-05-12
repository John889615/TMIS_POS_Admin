USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_DebtorMenuItems_update', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_DebtorMenuItems_update;
GO

CREATE PROCEDURE dbo.POS_DebtorMenuItems_update
    @DebtorMenuItemID INT,
    @FK_DebtorMenuID INT = NULL,
    @Item VARCHAR(50),
    @Description VARCHAR(255) = NULL,
    @FK_MenuItemID INT = NULL,
    @FK_ReferenceInsertID INT = NULL,
    @DateCreated DATETIME,
    @FK_CreatedUserID INT,
    @DateUpdated DATETIME = NULL,
    @FK_UpdatedUserID INT
AS
BEGIN
    UPDATE POS_DebtorMenuItems
    SET     FK_DebtorMenuID = @FK_DebtorMenuID,
    Item = @Item,
    Description = @Description,
    FK_MenuItemID = @FK_MenuItemID,
    FK_ReferenceInsertID = @FK_ReferenceInsertID,
    FK_CreatedUserID = @FK_CreatedUserID,
    DateUpdated = @DateUpdated,
    FK_UpdatedUserID = @FK_UpdatedUserID
    WHERE DebtorMenuItemID = @DebtorMenuItemID;

    SELECT *
    FROM POS_DebtorMenuItems
    WHERE DebtorMenuItemID = @DebtorMenuItemID;
END
GO