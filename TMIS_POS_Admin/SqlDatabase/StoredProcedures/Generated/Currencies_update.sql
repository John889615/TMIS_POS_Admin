USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.Currencies_update', 'P') IS NOT NULL
    DROP PROCEDURE dbo.Currencies_update;
GO

CREATE PROCEDURE dbo.Currencies_update
    @CurrencyID INT,
    @Currency VARCHAR(5),
    @Name VARCHAR(50),
    @ISO2Code VARCHAR(2) = NULL,
    @Symbol NVARCHAR(10) = NULL
AS
BEGIN
    UPDATE Currencies
    SET     Currency = @Currency,
    [Name] = @Name,
    ISO2Code = @ISO2Code,
    Symbol = @Symbol
    WHERE CurrencyID = @CurrencyID;

    SELECT *
    FROM Currencies
    WHERE CurrencyID = @CurrencyID;
END
GO