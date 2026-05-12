USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.EntityContacts_update', 'P') IS NOT NULL
    DROP PROCEDURE dbo.EntityContacts_update;
GO

CREATE PROCEDURE dbo.EntityContacts_update
    @EntityContactID INT,
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
    @DateUpdated DATETIME = NULL,
    @FK_CreatedUserID INT = NULL,
    @FK_UpdatedUserID INT = NULL
AS
BEGIN
    UPDATE EntityContacts
    SET     FK_EntityID = @FK_EntityID,
    EntityRecordID = @EntityRecordID,
    FK_ContactID = @FK_ContactID,
    IsPrimary = @IsPrimary,
    IsMarketing = @IsMarketing,
    IsEmergency = @IsEmergency,
    PreferredContactTime = @PreferredContactTime,
    PreferredLanguageCode = @PreferredLanguageCode,
    ValidFrom = @ValidFrom,
    ValidTo = @ValidTo,
    DateUpdated = @DateUpdated,
    FK_CreatedUserID = @FK_CreatedUserID,
    FK_UpdatedUserID = @FK_UpdatedUserID
    WHERE EntityContactID = @EntityContactID;

    SELECT *
    FROM EntityContacts
    WHERE EntityContactID = @EntityContactID;
END
GO