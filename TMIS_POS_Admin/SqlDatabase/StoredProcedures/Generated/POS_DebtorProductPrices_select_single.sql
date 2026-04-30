USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_DebtorProductPrices_select_single', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_DebtorProductPrices_select_single;
GO

CREATE PROCEDURE dbo.POS_DebtorProductPrices_select_single
    @DebtorProductPriceID INT
AS
BEGIN
    SELECT *
    FROM POS_DebtorProductPrices
    WHERE DebtorProductPriceID = @DebtorProductPriceID;
END
GO