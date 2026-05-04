USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_DebtorMenuItemProductPrinters_update', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_DebtorMenuItemProductPrinters_update;
GO

CREATE PROCEDURE dbo.POS_DebtorMenuItemProductPrinters_update
    @DebtorMenuItemProductPrinterID INT,
    @FK_MenuItemProductID INT = NULL,
    @FK_PrinterID INT,
    @FK_CreatedUserID INT,
    @FK_UpdatedUserID INT = NULL,
    @DateCreated DATETIME,
    @DateUpdated DATETIME
AS
BEGIN
    UPDATE POS_DebtorMenuItemProductPrinters
    SET     FK_MenuItemProductID = @FK_MenuItemProductID,
    FK_PrinterID = @FK_PrinterID,
    FK_CreatedUserID = @FK_CreatedUserID,
    FK_UpdatedUserID = @FK_UpdatedUserID,
    DateUpdated = @DateUpdated
    WHERE DebtorMenuItemProductPrinterID = @DebtorMenuItemProductPrinterID;

    SELECT *
    FROM POS_DebtorMenuItemProductPrinters
    WHERE DebtorMenuItemProductPrinterID = @DebtorMenuItemProductPrinterID;
END
GO