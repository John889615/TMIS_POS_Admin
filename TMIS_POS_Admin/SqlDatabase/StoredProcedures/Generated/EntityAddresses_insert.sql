USE [TMIS_Development]
GO

IF OBJECT_ID('dbo.EntityAddresses_insert', 'P') IS NOT NULL
    DROP PROCEDURE dbo.EntityAddresses_insert;
GO

CREATE PROCEDURE dbo.EntityAddresses_insert
    @FK_EntityID INT,
    @EntityRecordID INT,
    @FK_AddressID INT,
    @FK_AddressTypeID INT,
    @IsPrimary BIT,
    @ValidFrom DATE = NULL,
    @ValidTo DATE = NULL,
    @DateCreated DATETIME = NULL,
    @DateUpdated DATETIME = NULL,
    @FK_CreatedUserID INT = NULL,
    @FK_UpdatedUserID INT = NULL
AS
BEGIN
    DECLARE @Inserted TABLE (EntityAddressID INT);

    INSERT INTO EntityAddresses (FK_EntityID, EntityRecordID, FK_AddressID, FK_AddressTypeID, IsPrimary, ValidFrom, ValidTo, DateCreated, DateUpdated, FK_CreatedUserID, FK_UpdatedUserID)
    OUTPUT INSERTED.EntityAddressID INTO @Inserted
    VALUES (@FK_EntityID, @EntityRecordID, @FK_AddressID, @FK_AddressTypeID, @IsPrimary, @ValidFrom, @ValidTo, @DateCreated, @DateUpdated, @FK_CreatedUserID, @FK_UpdatedUserID);

    SELECT *
    FROM EntityAddresses
    WHERE EntityAddressID = 
    (
        SELECT TOP 1 EntityAddressID
        FROM @Inserted
    );
END
GO