USE [TMIS_Development]
GO

IF OBJECT_ID('dbo.Continents_insert', 'P') IS NOT NULL
    DROP PROCEDURE dbo.Continents_insert;
GO

CREATE PROCEDURE dbo.Continents_insert
    @Name VARCHAR(255),
    @ShortCode VARCHAR(2) = NULL,
    @FK_CreatedUserID INT = NULL,
    @FK_UpdatedUserID INT = NULL,
    @DateCreated DATETIME,
    @DateUpdated DATETIME = NULL
AS
BEGIN
    DECLARE @Inserted TABLE (ContinentID INT);

    INSERT INTO Continents ([Name], ShortCode, FK_CreatedUserID, FK_UpdatedUserID, DateCreated, DateUpdated)
    OUTPUT INSERTED.ContinentID INTO @Inserted
    VALUES (@Name, @ShortCode, @FK_CreatedUserID, @FK_UpdatedUserID, @DateCreated, @DateUpdated);

    SELECT *
    FROM Continents
    WHERE ContinentID = 
    (
        SELECT TOP 1 ContinentID
        FROM @Inserted
    );
END
GO