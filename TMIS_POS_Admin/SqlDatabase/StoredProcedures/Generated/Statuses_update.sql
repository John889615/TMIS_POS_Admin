USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.Statuses_update', 'P') IS NOT NULL
    DROP PROCEDURE dbo.Statuses_update;
GO

CREATE PROCEDURE dbo.Statuses_update
    @StatusID INT,
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
    UPDATE Statuses
    SET     FK_EntityID = @FK_EntityID,
    FK_StatusGroupID = @FK_StatusGroupID,
    SystemCode = @SystemCode,
    DisplayName = @DisplayName,
    IsActive = @IsActive,
    CanEdit = @CanEdit,
    ShowInUI = @ShowInUI,
    SortOrder = @SortOrder,
    DateUpdated = @DateUpdated
    WHERE StatusID = @StatusID;

    SELECT *
    FROM Statuses
    WHERE StatusID = @StatusID;
END
GO