USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.Countries_update', 'P') IS NOT NULL
    DROP PROCEDURE dbo.Countries_update;
GO

CREATE PROCEDURE dbo.Countries_update
    @CountryID INT,
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
    UPDATE Countries
    SET     CountryName = @CountryName,
    NativeName = @NativeName,
    OfficialName = @OfficialName,
    ISO2Code = @ISO2Code,
    ISO3Code = @ISO3Code,
    PrimaryLanguageCode = @PrimaryLanguageCode,
    NumericCode = @NumericCode,
    FK_DialingCodeID = @FK_DialingCodeID,
    FK_CurrencyID = @FK_CurrencyID,
    FK_ContinentID = @FK_ContinentID,
    FK_CreatedUserID = @FK_CreatedUserID,
    FK_UpdatedUserID = @FK_UpdatedUserID,
    DateUpdated = @DateUpdated
    WHERE CountryID = @CountryID;

    SELECT *
    FROM Countries
    WHERE CountryID = @CountryID;
END
GO