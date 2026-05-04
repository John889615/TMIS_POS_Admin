USE [TMIS_Development]
GO

IF OBJECT_ID('dbo.Guests_insert', 'P') IS NOT NULL
    DROP PROCEDURE dbo.Guests_insert;
GO

CREATE PROCEDURE dbo.Guests_insert
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
    DECLARE @Inserted TABLE (GuestID INT);

    INSERT INTO Guests (Title, FirstName, MiddleName, LastName, DateOfBirth, Gender, Nationality, PreferredLanguage, SpecialRequests, LoyaltyNumber, Notes, DateCreated, DateUpdated)
    OUTPUT INSERTED.GuestID INTO @Inserted
    VALUES (@Title, @FirstName, @MiddleName, @LastName, @DateOfBirth, @Gender, @Nationality, @PreferredLanguage, @SpecialRequests, @LoyaltyNumber, @Notes, @DateCreated, @DateUpdated);

    SELECT *
    FROM Guests
    WHERE GuestID = 
    (
        SELECT TOP 1 GuestID
        FROM @Inserted
    );
END
GO