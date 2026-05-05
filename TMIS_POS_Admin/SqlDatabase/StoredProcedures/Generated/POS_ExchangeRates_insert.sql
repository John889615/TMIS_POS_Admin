USE [TMIS_Development]
GO

IF OBJECT_ID('dbo.POS_ExchangeRates_insert', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_ExchangeRates_insert;
GO

CREATE PROCEDURE dbo.POS_ExchangeRates_insert
    @FK_CurrencyID INT,
    @ExchangeRate DECIMAL (18, 4),
    @DateCreated DATETIME,
    @DateUpdated DATETIME
AS
BEGIN
    DECLARE @Inserted TABLE (ExchangeRateID INT);

    INSERT INTO POS_ExchangeRates (FK_CurrencyID, ExchangeRate, DateCreated, DateUpdated)
    OUTPUT INSERTED.ExchangeRateID INTO @Inserted
    VALUES (@FK_CurrencyID, @ExchangeRate, @DateCreated, @DateUpdated);

    SELECT *
    FROM POS_ExchangeRates
    WHERE ExchangeRateID = 
    (
        SELECT TOP 1 ExchangeRateID
        FROM @Inserted
    );
END
GO