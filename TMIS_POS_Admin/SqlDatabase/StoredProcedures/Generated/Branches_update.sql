USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.Branches_update', 'P') IS NOT NULL
    DROP PROCEDURE dbo.Branches_update;
GO

CREATE PROCEDURE dbo.Branches_update
    @BranchID INT,
    @ShortCode VARCHAR(8),
    @Name VARCHAR(255),
    @FK_StatusID INT,
    @DateCreated DATETIME,
    @DateUpdated DATETIME = NULL
AS
BEGIN
    UPDATE Branches
    SET     ShortCode = @ShortCode,
    [Name] = @Name,
    FK_StatusID = @FK_StatusID,
    DateUpdated = @DateUpdated
    WHERE BranchID = @BranchID;

    SELECT *
    FROM Branches
    WHERE BranchID = @BranchID;
END
GO