USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_CostCenterPrinters_select_single', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_CostCenterPrinters_select_single;
GO

CREATE PROCEDURE dbo.POS_CostCenterPrinters_select_single
    @CostCenterPrinterID INT
AS
BEGIN
    SELECT *
    FROM POS_CostCenterPrinters
    WHERE CostCenterPrinterID = @CostCenterPrinterID;
END
GO