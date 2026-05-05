USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.Contacts_update', 'P') IS NOT NULL
    DROP PROCEDURE dbo.Contacts_update;
GO

CREATE PROCEDURE dbo.Contacts_update
    @ContactID INT,
    @ContactValue VARCHAR(255),
    @FK_ContactTypeID INT,
    @FK_DialingCodeID INT = NULL,
    @IsVerified BIT,
    @VerificationToken VARCHAR(100) = NULL,
    @VerifiedAt DATETIME = NULL,
    @Notes NVARCHAR(500) = NULL,
    @DateCreated DATETIME = NULL,
    @DateUpdated DATETIME = NULL
AS
BEGIN
    UPDATE Contacts
    SET     ContactValue = @ContactValue,
    FK_ContactTypeID = @FK_ContactTypeID,
    FK_DialingCodeID = @FK_DialingCodeID,
    IsVerified = @IsVerified,
    VerificationToken = @VerificationToken,
    VerifiedAt = @VerifiedAt,
    Notes = @Notes,
    DateUpdated = @DateUpdated
    WHERE ContactID = @ContactID;

    SELECT *
    FROM Contacts
    WHERE ContactID = @ContactID;
END
GO