USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.CountrySubregions_update', 'P') IS NOT NULL
    DROP PROCEDURE dbo.CountrySubregions_update;
GO

CREATE PROCEDURE dbo.CountrySubregions_update
    @CountrySubregionID INT,
    @Subregion VARCHAR(255),
    @FK_CountryRegionID INT
AS
BEGIN
    UPDATE CountrySubregions
    SET     Subregion = @Subregion,
    FK_CountryRegionID = @FK_CountryRegionID
    WHERE CountrySubregionID = @CountrySubregionID;

    SELECT *
    FROM CountrySubregions
    WHERE CountrySubregionID = @CountrySubregionID;
END
GO