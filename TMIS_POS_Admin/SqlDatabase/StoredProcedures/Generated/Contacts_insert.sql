USE [TMIS_Development]
GO

IF OBJECT_ID('dbo.Contacts_insert', 'P') IS NOT NULL
    DROP PROCEDURE dbo.Contacts_insert;
GO

CREATE PROCEDURE dbo.Contacts_insert
    @ContactValue VARCHAR(255),
    @FK_ContactTypeID INT,
    @FK_DialingCodeID INT = NULL,
    @IsVerified BIT,
    @VerificationToken VARCHAR(100) = NULL,
    @VerifiedAt DATETIME = NULL,
    @Notes NVARCHAR(500) = NULL,
    @DateCreated DATETIME = NULL,
    @DateUpdated DATETIME = NULL,
    @FK_CreatedUserID INT = NULL,
    @FK_UpdatedUserID INT = NULL
AS
BEGIN
    DECLARE @Inserted TABLE (ContactID INT);

    INSERT INTO Contacts (ContactValue, FK_ContactTypeID, FK_DialingCodeID, IsVerified, VerificationToken, VerifiedAt, Notes, DateCreated, DateUpdated, FK_CreatedUserID, FK_UpdatedUserID)
    OUTPUT INSERTED.ContactID INTO @Inserted
    VALUES (@ContactValue, @FK_ContactTypeID, @FK_DialingCodeID, @IsVerified, @VerificationToken, @VerifiedAt, @Notes, @DateCreated, @DateUpdated, @FK_CreatedUserID, @FK_UpdatedUserID);

    SELECT *
    FROM Contacts
    WHERE ContactID = 
    (
        SELECT TOP 1 ContactID
        FROM @Inserted
    );
END
GO