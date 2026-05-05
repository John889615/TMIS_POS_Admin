USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.CurrencyExchangeRates_update', 'P') IS NOT NULL
    DROP PROCEDURE dbo.CurrencyExchangeRates_update;
GO

CREATE PROCEDURE dbo.CurrencyExchangeRates_update
    @CurrencyExchangeRateID INT,
    @FK_FromCurrencyID INT,
    @FK_ToCurrencyID INT,
    @ExchangeRate DECIMAL (18, 4),
    @ConversionMethod VARCHAR(3),
    @EffectiveDate DATE,
    @Notes NVARCHAR(255) = NULL,
    @FK_CreatedUserID INT,
    @FK_UpdatedUserID INT = NULL,
    @DateCreated DATETIME,
    @DateUpdated DATETIME = NULL
AS
BEGIN
    UPDATE CurrencyExchangeRates
    SET     FK_FromCurrencyID = @FK_FromCurrencyID,
    FK_ToCurrencyID = @FK_ToCurrencyID,
    ExchangeRate = @ExchangeRate,
    ConversionMethod = @ConversionMethod,
    EffectiveDate = @EffectiveDate,
    Notes = @Notes,
    FK_CreatedUserID = @FK_CreatedUserID,
    FK_UpdatedUserID = @FK_UpdatedUserID,
    DateUpdated = @DateUpdated
    WHERE CurrencyExchangeRateID = @CurrencyExchangeRateID;

    SELECT *
    FROM CurrencyExchangeRates
    WHERE CurrencyExchangeRateID = @CurrencyExchangeRateID;
END
GO