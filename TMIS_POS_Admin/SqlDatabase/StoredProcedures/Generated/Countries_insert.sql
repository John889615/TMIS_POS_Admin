USE [TMIS_Development]
GO

IF OBJECT_ID('dbo.Countries_insert', 'P') IS NOT NULL
    DROP PROCEDURE dbo.Countries_insert;
GO

CREATE PROCEDURE dbo.Countries_insert
    @CountryName VARCHAR(255),
    @NativeName VARCHAR(255) = NULL,
    @OfficialName VARCHAR(255) = NULL,
    @ISO2Code VARCHAR(2),
    @ISO3Code VARCHAR(3),
    @PrimaryLanguageCode VARCHAR(20),
    @NumericCode SMALLINT = NULL,
    @FK_DialingCodeID INT = NULL,
    @FK_CurrencyID INT = NULL,
    @FK_ContinentID INT = NULL,
    @FK_CreatedUserID INT = NULL,
    @FK_UpdatedUserID INT = NULL,
    @DateCreated DATETIME,
    @DateUpdated DATETIME = NULL
AS
BEGIN
    DECLARE @Inserted TABLE (CountryID INT);

    INSERT INTO Countries (CountryName, NativeName, OfficialName, ISO2Code, ISO3Code, PrimaryLanguageCode, NumericCode, FK_DialingCodeID, FK_CurrencyID, FK_ContinentID, FK_CreatedUserID, FK_UpdatedUserID, DateCreated, DateUpdated)
    OUTPUT INSERTED.CountryID INTO @Inserted
    VALUES (@CountryName, @NativeName, @OfficialName, @ISO2Code, @ISO3Code, @PrimaryLanguageCode, @NumericCode, @FK_DialingCodeID, @FK_CurrencyID, @FK_ContinentID, @FK_CreatedUserID, @FK_UpdatedUserID, @DateCreated, @DateUpdated);

    SELECT *
    FROM Countries
    WHERE CountryID = 
    (
        SELECT TOP 1 CountryID
        FROM @Inserted
    );
END
GO