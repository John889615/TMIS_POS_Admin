USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_DebtorMenuPrinters_select_single', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_DebtorMenuPrinters_select_single;
GO

CREATE PROCEDURE dbo.POS_DebtorMenuPrinters_select_single
    @DebtorMenuPrinterID INT
AS
BEGIN
    SELECT *
    FROM POS_DebtorMenuPrinters
    WHERE DebtorMenuPrinterID = @DebtorMenuPrinterID;
END
GO