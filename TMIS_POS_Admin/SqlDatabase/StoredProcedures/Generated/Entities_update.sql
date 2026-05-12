USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.Entities_update', 'P') IS NOT NULL
    DROP PROCEDURE dbo.Entities_update;
GO

CREATE PROCEDURE dbo.Entities_update
    @EntityID INT,
    @Name VARCHAR(255),
    @DateCreated DATETIME = NULL,
    @DateUpdated DATETIME = NULL,
    @FK_CreatedUserID INT = NULL,
    @FK_UpdatedUserID INT = NULL
AS
BEGIN
    UPDATE Entities
    SET     [Name] = @Name,
    DateUpdated = @DateUpdated,
    FK_CreatedUserID = @FK_CreatedUserID,
    FK_UpdatedUserID = @FK_UpdatedUserID
    WHERE EntityID = @EntityID;

    SELECT *
    FROM Entities
    WHERE EntityID = @EntityID;
END
GO