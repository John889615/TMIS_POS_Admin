USE [TMIS_Development]
GO

IF OBJECT_ID('dbo.CurrencyExchangeRates_insert', 'P') IS NOT NULL
    DROP PROCEDURE dbo.CurrencyExchangeRates_insert;
GO

CREATE PROCEDURE dbo.CurrencyExchangeRates_insert
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
    DECLARE @Inserted TABLE (CurrencyExchangeRateID INT);

    INSERT INTO CurrencyExchangeRates (FK_FromCurrencyID, FK_ToCurrencyID, ExchangeRate, ConversionMethod, EffectiveDate, Notes, FK_CreatedUserID, FK_UpdatedUserID, DateCreated, DateUpdated)
    OUTPUT INSERTED.CurrencyExchangeRateID INTO @Inserted
    VALUES (@FK_FromCurrencyID, @FK_ToCurrencyID, @ExchangeRate, @ConversionMethod, @EffectiveDate, @Notes, @FK_CreatedUserID, @FK_UpdatedUserID, @DateCreated, @DateUpdated);

    SELECT *
    FROM CurrencyExchangeRates
    WHERE CurrencyExchangeRateID = 
    (
        SELECT TOP 1 CurrencyExchangeRateID
        FROM @Inserted
    );
END
GO