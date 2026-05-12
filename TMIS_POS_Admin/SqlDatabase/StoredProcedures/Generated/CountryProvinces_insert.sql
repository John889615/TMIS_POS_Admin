USE [TMIS_Development]
GO

IF OBJECT_ID('dbo.CountryProvinces_insert', 'P') IS NOT NULL
    DROP PROCEDURE dbo.CountryProvinces_insert;
GO

CREATE PROCEDURE dbo.CountryProvinces_insert
    @ProvinceName VARCHAR(100),
    @ISO2Code VARCHAR(2),
    @DateCreated DATETIME,
    @DateUpdated DATETIME = NULL,
    @FK_CountryID INT = NULL,
    @FK_CreatedUserID INT = NULL,
    @FK_UpdatedUserID INT = NULL
AS
BEGIN
    DECLARE @Inserted TABLE (CountryProvinceID INT);

    INSERT INTO CountryProvinces (ProvinceName, ISO2Code, DateCreated, DateUpdated, FK_CountryID, FK_CreatedUserID, FK_UpdatedUserID)
    OUTPUT INSERTED.CountryProvinceID INTO @Inserted
    VALUES (@ProvinceName, @ISO2Code, @DateCreated, @DateUpdated, @FK_CountryID, @FK_CreatedUserID, @FK_UpdatedUserID);

    SELECT *
    FROM CountryProvinces
    WHERE CountryProvinceID = 
    (
        SELECT TOP 1 CountryProvinceID
        FROM @Inserted
    );
END
GO