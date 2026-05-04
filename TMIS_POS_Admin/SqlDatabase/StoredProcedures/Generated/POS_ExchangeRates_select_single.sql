USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_ExchangeRates_select_single', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_ExchangeRates_select_single;
GO

CREATE PROCEDURE dbo.POS_ExchangeRates_select_single
    @ExchangeRateID INT
AS
BEGIN
    SELECT *
    FROM POS_ExchangeRates
    WHERE ExchangeRateID = @ExchangeRateID;
END
GO