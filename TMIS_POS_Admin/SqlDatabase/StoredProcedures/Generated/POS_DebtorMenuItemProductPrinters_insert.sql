USE [TMIS_Development]
GO

IF OBJECT_ID('dbo.POS_DebtorMenuItemProductPrinters_insert', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_DebtorMenuItemProductPrinters_insert;
GO

CREATE PROCEDURE dbo.POS_DebtorMenuItemProductPrinters_insert
    @FK_MenuItemProductID INT = NULL,
    @FK_PrinterID INT,
    @FK_CreatedUserID INT,
    @FK_UpdatedUserID INT = NULL,
    @DateCreated DATETIME,
    @DateUpdated DATETIME = NULL
AS
BEGIN
    DECLARE @Inserted TABLE (DebtorMenuItemProductPrinterID INT);

    INSERT INTO POS_DebtorMenuItemProductPrinters (FK_MenuItemProductID, FK_PrinterID, FK_CreatedUserID, FK_UpdatedUserID, DateCreated, DateUpdated)
    OUTPUT INSERTED.DebtorMenuItemProductPrinterID INTO @Inserted
    VALUES (@FK_MenuItemProductID, @FK_PrinterID, @FK_CreatedUserID, @FK_UpdatedUserID, @DateCreated, @DateUpdated);

    SELECT *
    FROM POS_DebtorMenuItemProductPrinters
    WHERE DebtorMenuItemProductPrinterID = 
    (
        SELECT TOP 1 DebtorMenuItemProductPrinterID
        FROM @Inserted
    );
END
GO