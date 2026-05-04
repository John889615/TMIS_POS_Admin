USE [TMIS_Development]
GO

IF OBJECT_ID('dbo.POS_DebtorMenuItems_insert', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_DebtorMenuItems_insert;
GO

CREATE PROCEDURE dbo.POS_DebtorMenuItems_insert
    @FK_DebtorMenuID INT = NULL,
    @Item VARCHAR(50),
    @Description VARCHAR(255) = NULL,
    @FK_MenuItemID INT = NULL,
    @FK_ReferenceInsertID INT = NULL,
    @DateCreated DATETIME,
    @FK_CreatedUserID INT,
    @DateUpdated DATETIME,
    @FK_UpdatedUserID INT
AS
BEGIN
    DECLARE @Inserted TABLE (DebtorMenuItemID INT);

    INSERT INTO POS_DebtorMenuItems (FK_DebtorMenuID, Item, Description, FK_MenuItemID, FK_ReferenceInsertID, DateCreated, FK_CreatedUserID, DateUpdated, FK_UpdatedUserID)
    OUTPUT INSERTED.DebtorMenuItemID INTO @Inserted
    VALUES (@FK_DebtorMenuID, @Item, @Description, @FK_MenuItemID, @FK_ReferenceInsertID, @DateCreated, @FK_CreatedUserID, @DateUpdated, @FK_UpdatedUserID);

    SELECT *
    FROM POS_DebtorMenuItems
    WHERE DebtorMenuItemID = 
    (
        SELECT TOP 1 DebtorMenuItemID
        FROM @Inserted
    );
END
GO