USE [TMIS_Development]
GO

IF OBJECT_ID('dbo.Addresses_insert', 'P') IS NOT NULL
    DROP PROCEDURE dbo.Addresses_insert;
GO

CREATE PROCEDURE dbo.Addresses_insert
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
    DECLARE @Inserted TABLE (AddressID INT);

    INSERT INTO Addresses (FK_CountryID, FK_ProvinceID, FK_AddressRegionID, StreetAddress, Locality, PostalCode, Landmark, Latitude, Longitude, Notes, DateCreated, DateUpdated, FK_CreatedUserID, FK_UpdatedUserID)
    OUTPUT INSERTED.AddressID INTO @Inserted
    VALUES (@FK_CountryID, @FK_ProvinceID, @FK_AddressRegionID, @StreetAddress, @Locality, @PostalCode, @Landmark, @Latitude, @Longitude, @Notes, @DateCreated, @DateUpdated, @FK_CreatedUserID, @FK_UpdatedUserID);

    SELECT *
    FROM Addresses
    WHERE AddressID = 
    (
        SELECT TOP 1 AddressID
        FROM @Inserted
    );
END
GO