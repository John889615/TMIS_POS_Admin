USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.Currencies_select_all', 'P') IS NOT NULL
    DROP PROCEDURE dbo.Currencies_select_all;
GO

CREATE PROCEDURE dbo.Currencies_select_all
AS
BEGIN
    SELECT *
    FROM Currencies;
END
GO