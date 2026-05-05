USE [TMIS_Development]
GO

IF OBJECT_ID('dbo.CountrySubregions_insert', 'P') IS NOT NULL
    DROP PROCEDURE dbo.CountrySubregions_insert;
GO

CREATE PROCEDURE dbo.CountrySubregions_insert
    @Subregion VARCHAR(255),
    @FK_CountryRegionID INT
AS
BEGIN
    DECLARE @Inserted TABLE (CountrySubregionID INT);

    INSERT INTO CountrySubregions (Subregion, FK_CountryRegionID)
    OUTPUT INSERTED.CountrySubregionID INTO @Inserted
    VALUES (@Subregion, @FK_CountryRegionID);

    SELECT *
    FROM CountrySubregions
    WHERE CountrySubregionID = 
    (
        SELECT TOP 1 CountrySubregionID
        FROM @Inserted
    );
END
GO