USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_CostCenterProductPriceHistory_select_single', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_CostCenterProductPriceHistory_select_single;
GO

CREATE PROCEDURE dbo.POS_CostCenterProductPriceHistory_select_single
    @CostcenterProductPriceHistoryID INT
AS
BEGIN
    SELECT *
    FROM POS_CostCenterProductPriceHistory
    WHERE CostcenterProductPriceHistoryID = @CostcenterProductPriceHistoryID;
END
GO