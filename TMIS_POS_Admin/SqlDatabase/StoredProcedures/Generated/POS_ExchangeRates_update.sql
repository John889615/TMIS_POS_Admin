USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_ExchangeRates_update', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_ExchangeRates_update;
GO

CREATE PROCEDURE dbo.POS_ExchangeRates_update
    @ExchangeRateID INT,
    @FK_CurrencyID INT,
    @ExchangeRate DECIMAL (18, 4),
    @DateCreated DATETIME,
    @DateUpdated DATETIME = NULL
AS
BEGIN
    UPDATE POS_ExchangeRates
    SET     FK_CurrencyID = @FK_CurrencyID,
    ExchangeRate = @ExchangeRate,
    DateUpdated = @DateUpdated
    WHERE ExchangeRateID = @ExchangeRateID;

    SELECT *
    FROM POS_ExchangeRates
    WHERE ExchangeRateID = @ExchangeRateID;
END
GO