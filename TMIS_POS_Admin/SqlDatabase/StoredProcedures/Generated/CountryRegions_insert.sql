USE [TMIS_Development]
GO

IF OBJECT_ID('dbo.CountryRegions_insert', 'P') IS NOT NULL
    DROP PROCEDURE dbo.CountryRegions_insert;
GO

CREATE PROCEDURE dbo.CountryRegions_insert
    @Region VARCHAR(255),
    @FK_CreatedUserID INT = NULL,
    @FK_UpdatedUserID INT = NULL,
    @DateCreated DATETIME,
    @DateUpdated DATETIME = NULL,
    @FK_CountryID INT = NULL
AS
BEGIN
    DECLARE @Inserted TABLE (CountryRegionID INT);

    INSERT INTO CountryRegions (Region, FK_CreatedUserID, FK_UpdatedUserID, DateCreated, DateUpdated, FK_CountryID)
    OUTPUT INSERTED.CountryRegionID INTO @Inserted
    VALUES (@Region, @FK_CreatedUserID, @FK_UpdatedUserID, @DateCreated, @DateUpdated, @FK_CountryID);

    SELECT *
    FROM CountryRegions
    WHERE CountryRegionID = 
    (
        SELECT TOP 1 CountryRegionID
        FROM @Inserted
    );
END
GO