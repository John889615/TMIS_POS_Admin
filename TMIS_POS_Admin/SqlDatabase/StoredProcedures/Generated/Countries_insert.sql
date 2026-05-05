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
    @FK_CountryRegionID INT = NULL,
    @FK_CountrySubregionID INT = NULL,
    @FK_TimeZoneID INT = NULL
AS
BEGIN
    DECLARE @Inserted TABLE (CountryID INT);

    INSERT INTO Countries (CountryName, NativeName, OfficialName, ISO2Code, ISO3Code, PrimaryLanguageCode, NumericCode, FK_DialingCodeID, FK_CurrencyID, FK_CountryRegionID, FK_CountrySubregionID, FK_TimeZoneID)
    OUTPUT INSERTED.CountryID INTO @Inserted
    VALUES (@CountryName, @NativeName, @OfficialName, @ISO2Code, @ISO3Code, @PrimaryLanguageCode, @NumericCode, @FK_DialingCodeID, @FK_CurrencyID, @FK_CountryRegionID, @FK_CountrySubregionID, @FK_TimeZoneID);

    SELECT *
    FROM Countries
    WHERE CountryID = 
    (
        SELECT TOP 1 CountryID
        FROM @Inserted
    );
END
GO