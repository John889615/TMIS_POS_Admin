USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.AddressRegions_update', 'P') IS NOT NULL
    DROP PROCEDURE dbo.AddressRegions_update;
GO

CREATE PROCEDURE dbo.AddressRegions_update
    @AddressRegionID INT,
    @RegionName VARCHAR(255),
    @Description NVARCHAR(1000) = NULL,
    @FK_CountryID INT = NULL,
    @FK_ProvinceID INT = NULL,
    @DateCreated DATETIME,
    @DateUpdated DATETIME
AS
BEGIN
    UPDATE AddressRegions
    SET     RegionName = @RegionName,
    Description = @Description,
    FK_CountryID = @FK_CountryID,
    FK_ProvinceID = @FK_ProvinceID,
    DateUpdated = @DateUpdated
    WHERE AddressRegionID = @AddressRegionID;

    SELECT *
    FROM AddressRegions
    WHERE AddressRegionID = @AddressRegionID;
END
GO