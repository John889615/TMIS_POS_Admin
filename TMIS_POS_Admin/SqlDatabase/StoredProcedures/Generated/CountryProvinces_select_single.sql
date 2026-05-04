USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.CountryProvinces_select_single', 'P') IS NOT NULL
    DROP PROCEDURE dbo.CountryProvinces_select_single;
GO

CREATE PROCEDURE dbo.CountryProvinces_select_single
    @CountryProvinceID INT
AS
BEGIN
    SELECT *
    FROM CountryProvinces
    WHERE CountryProvinceID = @CountryProvinceID;
END
GO