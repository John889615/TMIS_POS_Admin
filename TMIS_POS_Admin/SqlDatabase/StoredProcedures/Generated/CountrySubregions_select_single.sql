USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.CountrySubregions_select_single', 'P') IS NOT NULL
    DROP PROCEDURE dbo.CountrySubregions_select_single;
GO

CREATE PROCEDURE dbo.CountrySubregions_select_single
    @CountrySubregionID INT
AS
BEGIN
    SELECT *
    FROM CountrySubregions
    WHERE CountrySubregionID = @CountrySubregionID;
END
GO