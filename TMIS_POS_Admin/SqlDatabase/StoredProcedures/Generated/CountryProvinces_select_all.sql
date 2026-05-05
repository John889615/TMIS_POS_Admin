USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.CountryProvinces_select_all', 'P') IS NOT NULL
    DROP PROCEDURE dbo.CountryProvinces_select_all;
GO

CREATE PROCEDURE dbo.CountryProvinces_select_all
AS
BEGIN
    SELECT *
    FROM CountryProvinces;
END
GO