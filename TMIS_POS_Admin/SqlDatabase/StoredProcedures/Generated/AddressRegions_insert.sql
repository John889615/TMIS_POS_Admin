USE [TMIS_Development]
GO

IF OBJECT_ID('dbo.AddressRegions_insert', 'P') IS NOT NULL
    DROP PROCEDURE dbo.AddressRegions_insert;
GO

CREATE PROCEDURE dbo.AddressRegions_insert
    @RegionName VARCHAR(255),
    @Description NVARCHAR(1000) = NULL,
    @FK_CountryID INT = NULL,
    @FK_ProvinceID INT = NULL,
    @DateCreated DATETIME,
    @DateUpdated DATETIME = NULL,
    @FK_CreatedUserID INT = NULL,
    @FK_UpdatedUserID INT = NULL
AS
BEGIN
    DECLARE @Inserted TABLE (AddressRegionID INT);

    INSERT INTO AddressRegions (RegionName, Description, FK_CountryID, FK_ProvinceID, DateCreated, DateUpdated, FK_CreatedUserID, FK_UpdatedUserID)
    OUTPUT INSERTED.AddressRegionID INTO @Inserted
    VALUES (@RegionName, @Description, @FK_CountryID, @FK_ProvinceID, @DateCreated, @DateUpdated, @FK_CreatedUserID, @FK_UpdatedUserID);

    SELECT *
    FROM AddressRegions
    WHERE AddressRegionID = 
    (
        SELECT TOP 1 AddressRegionID
        FROM @Inserted
    );
END
GO