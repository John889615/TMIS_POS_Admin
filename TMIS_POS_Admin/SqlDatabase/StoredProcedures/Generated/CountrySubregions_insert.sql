USE [TMIS_Development]
GO

IF OBJECT_ID('dbo.CountrySubregions_insert', 'P') IS NOT NULL
    DROP PROCEDURE dbo.CountrySubregions_insert;
GO

CREATE PROCEDURE dbo.CountrySubregions_insert
    @Subregion VARCHAR(255),
    @FK_CountryRegionID INT,
    @FK_CreatedUserID INT = NULL,
    @FK_UpdatedUserID INT = NULL,
    @DateCreated DATETIME,
    @DateUpdated DATETIME = NULL
AS
BEGIN
    DECLARE @Inserted TABLE (CountrySubregionID INT);

    INSERT INTO CountrySubregions (Subregion, FK_CountryRegionID, FK_CreatedUserID, FK_UpdatedUserID, DateCreated, DateUpdated)
    OUTPUT INSERTED.CountrySubregionID INTO @Inserted
    VALUES (@Subregion, @FK_CountryRegionID, @FK_CreatedUserID, @FK_UpdatedUserID, @DateCreated, @DateUpdated);

    SELECT *
    FROM CountrySubregions
    WHERE CountrySubregionID = 
    (
        SELECT TOP 1 CountrySubregionID
        FROM @Inserted
    );
END
GO