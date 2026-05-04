USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.CountryProvinces_update', 'P') IS NOT NULL
    DROP PROCEDURE dbo.CountryProvinces_update;
GO

CREATE PROCEDURE dbo.CountryProvinces_update
    @CountryProvinceID INT,
    @ProvinceName VARCHAR(100),
    @ISO2Code VARCHAR(2),
    @DateCreated DATETIME,
    @DateUpdated DATETIME,
    @FK_CountryID INT = NULL
AS
BEGIN
    UPDATE CountryProvinces
    SET     ProvinceName = @ProvinceName,
    ISO2Code = @ISO2Code,
    DateUpdated = @DateUpdated,
    FK_CountryID = @FK_CountryID
    WHERE CountryProvinceID = @CountryProvinceID;

    SELECT *
    FROM CountryProvinces
    WHERE CountryProvinceID = @CountryProvinceID;
END
GO