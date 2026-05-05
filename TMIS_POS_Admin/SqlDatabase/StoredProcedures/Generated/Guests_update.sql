USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.Guests_update', 'P') IS NOT NULL
    DROP PROCEDURE dbo.Guests_update;
GO

CREATE PROCEDURE dbo.Guests_update
    @GuestID INT,
    @Title VARCHAR(10) = NULL,
    @FirstName VARCHAR(50),
    @MiddleName VARCHAR(50) = NULL,
    @LastName VARCHAR(50),
    @DateOfBirth DATE = NULL,
    @Gender VARCHAR(20) = NULL,
    @Nationality VARCHAR(50) = NULL,
    @PreferredLanguage VARCHAR(20) = NULL,
    @SpecialRequests VARCHAR(MAX) = NULL,
    @LoyaltyNumber VARCHAR(50) = NULL,
    @Notes VARCHAR(MAX) = NULL,
    @DateCreated DATETIME = NULL,
    @DateUpdated DATETIME = NULL
AS
BEGIN
    UPDATE Guests
    SET     Title = @Title,
    FirstName = @FirstName,
    MiddleName = @MiddleName,
    LastName = @LastName,
    DateOfBirth = @DateOfBirth,
    Gender = @Gender,
    Nationality = @Nationality,
    PreferredLanguage = @PreferredLanguage,
    SpecialRequests = @SpecialRequests,
    LoyaltyNumber = @LoyaltyNumber,
    Notes = @Notes,
    DateUpdated = @DateUpdated
    WHERE GuestID = @GuestID;

    SELECT *
    FROM Guests
    WHERE GuestID = @GuestID;
END
GO