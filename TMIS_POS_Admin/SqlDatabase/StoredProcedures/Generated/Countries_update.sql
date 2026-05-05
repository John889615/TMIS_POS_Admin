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
    @FK_CountryRegionID INT = NULL,
    @FK_CountrySubregionID INT = NULL,
    @FK_TimeZoneID INT = NULL
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
    FK_CountryRegionID = @FK_CountryRegionID,
    FK_CountrySubregionID = @FK_CountrySubregionID,
    FK_TimeZoneID = @FK_TimeZoneID
    WHERE CountryID = @CountryID;

    SELECT *
    FROM Countries
    WHERE CountryID = @CountryID;
END
GO