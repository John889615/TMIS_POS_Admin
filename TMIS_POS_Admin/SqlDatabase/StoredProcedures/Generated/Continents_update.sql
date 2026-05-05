USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.Continents_update', 'P') IS NOT NULL
    DROP PROCEDURE dbo.Continents_update;
GO

CREATE PROCEDURE dbo.Continents_update
    @ContinentID INT,
    @Name VARCHAR(255),
    @ShortCode VARCHAR(2) = NULL
AS
BEGIN
    UPDATE Continents
    SET     [Name] = @Name,
    ShortCode = @ShortCode
    WHERE ContinentID = @ContinentID;

    SELECT *
    FROM Continents
    WHERE ContinentID = @ContinentID;
END
GO