USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.CurrencyExchangeRates_select_all', 'P') IS NOT NULL
    DROP PROCEDURE dbo.CurrencyExchangeRates_select_all;
GO

CREATE PROCEDURE dbo.CurrencyExchangeRates_select_all
AS
BEGIN
    SELECT *
    FROM CurrencyExchangeRates;
END
GO