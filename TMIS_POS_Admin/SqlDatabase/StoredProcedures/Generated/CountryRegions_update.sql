USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.CountryRegions_update', 'P') IS NOT NULL
    DROP PROCEDURE dbo.CountryRegions_update;
GO

CREATE PROCEDURE dbo.CountryRegions_update
    @CountryRegionID INT,
    @Region VARCHAR(255),
    @FK_ContinentID INT
AS
BEGIN
    UPDATE CountryRegions
    SET     Region = @Region,
    FK_ContinentID = @FK_ContinentID
    WHERE CountryRegionID = @CountryRegionID;

    SELECT *
    FROM CountryRegions
    WHERE CountryRegionID = @CountryRegionID;
END
GO