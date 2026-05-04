USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_SlipPrinters_select_single', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_SlipPrinters_select_single;
GO

CREATE PROCEDURE dbo.POS_SlipPrinters_select_single
    @SlipPrinterID INT
AS
BEGIN
    SELECT *
    FROM POS_SlipPrinters
    WHERE SlipPrinterID = @SlipPrinterID;
END
GO