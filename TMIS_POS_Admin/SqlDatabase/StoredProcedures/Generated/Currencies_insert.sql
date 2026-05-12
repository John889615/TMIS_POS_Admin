USE [TMIS_Development]
GO

IF OBJECT_ID('dbo.Currencies_insert', 'P') IS NOT NULL
    DROP PROCEDURE dbo.Currencies_insert;
GO

CREATE PROCEDURE dbo.Currencies_insert
    @Currency VARCHAR(5),
    @Name VARCHAR(50),
    @ISO2Code VARCHAR(2) = NULL,
    @Symbol NVARCHAR(10) = NULL,
    @FK_CreatedUserID INT = NULL,
    @FK_UpdatedUserID INT = NULL,
    @DateCreated DATETIME,
    @DateUpdated DATETIME = NULL
AS
BEGIN
    DECLARE @Inserted TABLE (CurrencyID INT);

    INSERT INTO Currencies (Currency, [Name], ISO2Code, Symbol, FK_CreatedUserID, FK_UpdatedUserID, DateCreated, DateUpdated)
    OUTPUT INSERTED.CurrencyID INTO @Inserted
    VALUES (@Currency, @Name, @ISO2Code, @Symbol, @FK_CreatedUserID, @FK_UpdatedUserID, @DateCreated, @DateUpdated);

    SELECT *
    FROM Currencies
    WHERE CurrencyID = 
    (
        SELECT TOP 1 CurrencyID
        FROM @Inserted
    );
END
GO