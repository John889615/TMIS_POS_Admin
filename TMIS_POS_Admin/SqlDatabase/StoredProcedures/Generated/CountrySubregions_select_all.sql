USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.CountrySubregions_select_all', 'P') IS NOT NULL
    DROP PROCEDURE dbo.CountrySubregions_select_all;
GO

CREATE PROCEDURE dbo.CountrySubregions_select_all
AS
BEGIN
    SELECT *
    FROM CountrySubregions;
END
GO