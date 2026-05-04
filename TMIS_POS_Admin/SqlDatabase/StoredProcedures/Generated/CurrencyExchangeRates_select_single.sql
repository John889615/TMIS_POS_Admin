USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.CurrencyExchangeRates_select_single', 'P') IS NOT NULL
    DROP PROCEDURE dbo.CurrencyExchangeRates_select_single;
GO

CREATE PROCEDURE dbo.CurrencyExchangeRates_select_single
    @CurrencyExchangeRateID INT
AS
BEGIN
    SELECT *
    FROM CurrencyExchangeRates
    WHERE CurrencyExchangeRateID = @CurrencyExchangeRateID;
END
GO