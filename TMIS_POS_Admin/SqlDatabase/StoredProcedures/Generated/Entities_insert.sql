USE [TMIS_Development]
GO

IF OBJECT_ID('dbo.Entities_insert', 'P') IS NOT NULL
    DROP PROCEDURE dbo.Entities_insert;
GO

CREATE PROCEDURE dbo.Entities_insert
    @Name VARCHAR(255),
    @DateCreated DATETIME = NULL,
    @DateUpdated DATETIME = NULL,
    @FK_CreatedUserID INT = NULL,
    @FK_UpdatedUserID INT = NULL
AS
BEGIN
    DECLARE @Inserted TABLE (EntityID INT);

    INSERT INTO Entities ([Name], DateCreated, DateUpdated, FK_CreatedUserID, FK_UpdatedUserID)
    OUTPUT INSERTED.EntityID INTO @Inserted
    VALUES (@Name, @DateCreated, @DateUpdated, @FK_CreatedUserID, @FK_UpdatedUserID);

    SELECT *
    FROM Entities
    WHERE EntityID = 
    (
        SELECT TOP 1 EntityID
        FROM @Inserted
    );
END
GO