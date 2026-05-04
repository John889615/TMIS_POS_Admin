USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.CountryRegions_select_single', 'P') IS NOT NULL
    DROP PROCEDURE dbo.CountryRegions_select_single;
GO

CREATE PROCEDURE dbo.CountryRegions_select_single
    @CountryRegionID INT
AS
BEGIN
    SELECT *
    FROM CountryRegions
    WHERE CountryRegionID = @CountryRegionID;
END
GO