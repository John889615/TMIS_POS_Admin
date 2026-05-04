USE [TMIS_Development]
GO

IF OBJECT_ID('dbo.EntityContacts_insert', 'P') IS NOT NULL
    DROP PROCEDURE dbo.EntityContacts_insert;
GO

CREATE PROCEDURE dbo.EntityContacts_insert
    @FK_EntityID INT,
    @EntityRecordID INT,
    @FK_ContactID INT,
    @IsPrimary BIT,
    @IsMarketing BIT,
    @IsEmergency BIT,
    @PreferredContactTime VARCHAR(100) = NULL,
    @PreferredLanguageCode VARCHAR(10) = NULL,
    @ValidFrom DATE = NULL,
    @ValidTo DATE = NULL,
    @DateCreated DATETIME = NULL,
    @DateUpdated DATETIME = NULL
AS
BEGIN
    DECLARE @Inserted TABLE (EntityContactID INT);

    INSERT INTO EntityContacts (FK_EntityID, EntityRecordID, FK_ContactID, IsPrimary, IsMarketing, IsEmergency, PreferredContactTime, PreferredLanguageCode, ValidFrom, ValidTo, DateCreated, DateUpdated)
    OUTPUT INSERTED.EntityContactID INTO @Inserted
    VALUES (@FK_EntityID, @EntityRecordID, @FK_ContactID, @IsPrimary, @IsMarketing, @IsEmergency, @PreferredContactTime, @PreferredLanguageCode, @ValidFrom, @ValidTo, @DateCreated, @DateUpdated);

    SELECT *
    FROM EntityContacts
    WHERE EntityContactID = 
    (
        SELECT TOP 1 EntityContactID
        FROM @Inserted
    );
END
GO