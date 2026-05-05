USE [TMIS_Development]
GO

IF OBJECT_ID('dbo.Currencies_insert', 'P') IS NOT NULL
    DROP PROCEDURE dbo.Currencies_insert;
GO

CREATE PROCEDURE dbo.Currencies_insert
    @Currency VARCHAR(5),
    @Name VARCHAR(50),
    @ISO2Code VARCHAR(2) = NULL,
    @Symbol NVARCHAR(10) = NULL
AS
BEGIN
    DECLARE @Inserted TABLE (CurrencyID INT);

    INSERT INTO Currencies (Currency, [Name], ISO2Code, Symbol)
    OUTPUT INSERTED.CurrencyID INTO @Inserted
    VALUES (@Currency, @Name, @ISO2Code, @Symbol);

    SELECT *
    FROM Currencies
    WHERE CurrencyID = 
    (
        SELECT TOP 1 CurrencyID
        FROM @Inserted
    );
END
GO