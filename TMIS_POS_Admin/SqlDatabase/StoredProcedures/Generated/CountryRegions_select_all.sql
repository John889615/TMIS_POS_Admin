USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.CountryRegions_select_all', 'P') IS NOT NULL
    DROP PROCEDURE dbo.CountryRegions_select_all;
GO

CREATE PROCEDURE dbo.CountryRegions_select_all
AS
BEGIN
    SELECT *
    FROM CountryRegions;
END
GO