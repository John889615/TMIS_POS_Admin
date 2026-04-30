USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_DebtorProductPriceHistory_select_single', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_DebtorProductPriceHistory_select_single;
GO

CREATE PROCEDURE dbo.POS_DebtorProductPriceHistory_select_single
    @DebtorProductPriceHistoryID INT
AS
BEGIN
    SELECT *
    FROM POS_DebtorProductPriceHistory
    WHERE DebtorProductPriceHistoryID = @DebtorProductPriceHistoryID;
END
GO