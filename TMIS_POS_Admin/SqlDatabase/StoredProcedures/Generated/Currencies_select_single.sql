USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.Currencies_select_single', 'P') IS NOT NULL
    DROP PROCEDURE dbo.Currencies_select_single;
GO

CREATE PROCEDURE dbo.Currencies_select_single
    @CurrencyID INT
AS
BEGIN
    SELECT *
    FROM Currencies
    WHERE CurrencyID = @CurrencyID;
END
GO