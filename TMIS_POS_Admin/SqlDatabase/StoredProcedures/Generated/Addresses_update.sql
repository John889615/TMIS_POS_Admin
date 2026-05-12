USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.Addresses_update', 'P') IS NOT NULL
    DROP PROCEDURE dbo.Addresses_update;
GO

CREATE PROCEDURE dbo.Addresses_update
    @AddressID INT,
    @FK_CountryID INT,
    @FK_ProvinceID INT = NULL,
    @FK_AddressRegionID INT = NULL,
    @StreetAddress VARCHAR(255) = NULL,
    @Locality VARCHAR(255) = NULL,
    @PostalCode VARCHAR(20) = NULL,
    @Landmark VARCHAR(255) = NULL,
    @Latitude DECIMAL (18, 4) = NULL,
    @Longitude DECIMAL (18, 4) = NULL,
    @Notes NVARCHAR(1000) = NULL,
    @DateCreated DATETIME = NULL,
    @DateUpdated DATETIME = NULL,
    @FK_CreatedUserID INT = NULL,
    @FK_UpdatedUserID INT = NULL
AS
BEGIN
    UPDATE Addresses
    SET     FK_CountryID = @FK_CountryID,
    FK_ProvinceID = @FK_ProvinceID,
    FK_AddressRegionID = @FK_AddressRegionID,
    StreetAddress = @StreetAddress,
    Locality = @Locality,
    PostalCode = @PostalCode,
    Landmark = @Landmark,
    Latitude = @Latitude,
    Longitude = @Longitude,
    Notes = @Notes,
    DateUpdated = @DateUpdated,
    FK_CreatedUserID = @FK_CreatedUserID,
    FK_UpdatedUserID = @FK_UpdatedUserID
    WHERE AddressID = @AddressID;

    SELECT *
    FROM Addresses
    WHERE AddressID = @AddressID;
END
GO