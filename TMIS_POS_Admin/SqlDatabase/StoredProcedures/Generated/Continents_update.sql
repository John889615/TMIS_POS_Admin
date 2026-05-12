USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.Continents_update', 'P') IS NOT NULL
    DROP PROCEDURE dbo.Continents_update;
GO

CREATE PROCEDURE dbo.Continents_update
    @ContinentID INT,
    @Name VARCHAR(255),
    @ShortCode VARCHAR(2) = NULL,
    @FK_CreatedUserID INT = NULL,
    @FK_UpdatedUserID INT = NULL,
    @DateCreated DATETIME,
    @DateUpdated DATETIME = NULL
AS
BEGIN
    UPDATE Continents
    SET     [Name] = @Name,
    ShortCode = @ShortCode,
    FK_CreatedUserID = @FK_CreatedUserID,
    FK_UpdatedUserID = @FK_UpdatedUserID,
    DateUpdated = @DateUpdated
    WHERE ContinentID = @ContinentID;

    SELECT *
    FROM Continents
    WHERE ContinentID = @ContinentID;
END
GO