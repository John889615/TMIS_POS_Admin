USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.CountryRegions_update', 'P') IS NOT NULL
    DROP PROCEDURE dbo.CountryRegions_update;
GO

CREATE PROCEDURE dbo.CountryRegions_update
    @CountryRegionID INT,
    @Region VARCHAR(255),
    @FK_CreatedUserID INT = NULL,
    @FK_UpdatedUserID INT = NULL,
    @DateCreated DATETIME,
    @DateUpdated DATETIME = NULL,
    @FK_CountryID INT = NULL
AS
BEGIN
    UPDATE CountryRegions
    SET     Region = @Region,
    FK_CreatedUserID = @FK_CreatedUserID,
    FK_UpdatedUserID = @FK_UpdatedUserID,
    DateUpdated = @DateUpdated,
    FK_CountryID = @FK_CountryID
    WHERE CountryRegionID = @CountryRegionID;

    SELECT *
    FROM CountryRegions
    WHERE CountryRegionID = @CountryRegionID;
END
GO