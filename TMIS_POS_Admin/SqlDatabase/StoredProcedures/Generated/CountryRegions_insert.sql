USE [TMIS_Development]
GO

IF OBJECT_ID('dbo.CountryRegions_insert', 'P') IS NOT NULL
    DROP PROCEDURE dbo.CountryRegions_insert;
GO

CREATE PROCEDURE dbo.CountryRegions_insert
    @Region VARCHAR(255),
    @FK_ContinentID INT
AS
BEGIN
    DECLARE @Inserted TABLE (CountryRegionID INT);

    INSERT INTO CountryRegions (Region, FK_ContinentID)
    OUTPUT INSERTED.CountryRegionID INTO @Inserted
    VALUES (@Region, @FK_ContinentID);

    SELECT *
    FROM CountryRegions
    WHERE CountryRegionID = 
    (
        SELECT TOP 1 CountryRegionID
        FROM @Inserted
    );
END
GO