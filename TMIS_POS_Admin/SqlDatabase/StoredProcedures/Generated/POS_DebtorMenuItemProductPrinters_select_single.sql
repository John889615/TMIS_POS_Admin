USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_DebtorMenuItemProductPrinters_select_single', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_DebtorMenuItemProductPrinters_select_single;
GO

CREATE PROCEDURE dbo.POS_DebtorMenuItemProductPrinters_select_single
    @DebtorMenuItemProductPrinterID INT
AS
BEGIN
    SELECT *
    FROM POS_DebtorMenuItemProductPrinters
    WHERE DebtorMenuItemProductPrinterID = @DebtorMenuItemProductPrinterID;
END
GO