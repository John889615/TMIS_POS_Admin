USE [TMIS_Development]
GO

IF OBJECT_ID('dbo.Statuses_insert', 'P') IS NOT NULL
    DROP PROCEDURE dbo.Statuses_insert;
GO

CREATE PROCEDURE dbo.Statuses_insert
    @FK_EntityID INT,
    @FK_StatusGroupID INT,
    @SystemCode VARCHAR(50),
    @DisplayName VARCHAR(100),
    @IsActive BIT,
    @CanEdit BIT,
    @ShowInUI BIT,
    @SortOrder INT = NULL,
    @DateCreated DATETIME = NULL,
    @DateUpdated DATETIME = NULL
AS
BEGIN
    DECLARE @Inserted TABLE (StatusID INT);

    INSERT INTO Statuses (FK_EntityID, FK_StatusGroupID, SystemCode, DisplayName, IsActive, CanEdit, ShowInUI, SortOrder, DateCreated, DateUpdated)
    OUTPUT INSERTED.StatusID INTO @Inserted
    VALUES (@FK_EntityID, @FK_StatusGroupID, @SystemCode, @DisplayName, @IsActive, @CanEdit, @ShowInUI, @SortOrder, @DateCreated, @DateUpdated);

    SELECT *
    FROM Statuses
    WHERE StatusID = 
    (
        SELECT TOP 1 StatusID
        FROM @Inserted
    );
END
GO