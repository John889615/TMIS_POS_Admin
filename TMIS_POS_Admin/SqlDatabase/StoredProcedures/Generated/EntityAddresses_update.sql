USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.EntityAddresses_update', 'P') IS NOT NULL
    DROP PROCEDURE dbo.EntityAddresses_update;
GO

CREATE PROCEDURE dbo.EntityAddresses_update
    @EntityAddressID INT,
    @FK_EntityID INT,
    @EntityRecordID INT,
    @FK_AddressID INT,
    @FK_AddressTypeID INT,
    @IsPrimary BIT,
    @ValidFrom DATE = NULL,
    @ValidTo DATE = NULL,
    @DateCreated DATETIME = NULL,
    @DateUpdated DATETIME = NULL
AS
BEGIN
    UPDATE EntityAddresses
    SET     FK_EntityID = @FK_EntityID,
    EntityRecordID = @EntityRecordID,
    FK_AddressID = @FK_AddressID,
    FK_AddressTypeID = @FK_AddressTypeID,
    IsPrimary = @IsPrimary,
    ValidFrom = @ValidFrom,
    ValidTo = @ValidTo,
    DateUpdated = @DateUpdated
    WHERE EntityAddressID = @EntityAddressID;

    SELECT *
    FROM EntityAddresses
    WHERE EntityAddressID = @EntityAddressID;
END
GO