USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_LocationCurrencies_select_single', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_LocationCurrencies_select_single;
GO

CREATE PROCEDURE dbo.POS_LocationCurrencies_select_single
    @LocationCurrencyID INT
AS
BEGIN
    SELECT *
    FROM POS_LocationCurrencies
    WHERE LocationCurrencyID = @LocationCurrencyID;
END
GO