USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.Countries_select_single', 'P') IS NOT NULL
    DROP PROCEDURE dbo.Countries_select_single;
GO

CREATE PROCEDURE dbo.Countries_select_single
    @CountryID INT
AS
BEGIN
    SELECT *
    FROM Countries
    WHERE CountryID = @CountryID;
END
GO