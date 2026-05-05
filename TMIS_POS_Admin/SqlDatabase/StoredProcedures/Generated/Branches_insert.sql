USE [TMIS_Development]
GO

IF OBJECT_ID('dbo.Branches_insert', 'P') IS NOT NULL
    DROP PROCEDURE dbo.Branches_insert;
GO

CREATE PROCEDURE dbo.Branches_insert
    @ShortCode VARCHAR(8),
    @Name VARCHAR(255),
    @FK_StatusID INT,
    @DateCreated DATETIME,
    @DateUpdated DATETIME
AS
BEGIN
    DECLARE @Inserted TABLE (BranchID INT);

    INSERT INTO Branches (ShortCode, [Name], FK_StatusID, DateCreated, DateUpdated)
    OUTPUT INSERTED.BranchID INTO @Inserted
    VALUES (@ShortCode, @Name, @FK_StatusID, @DateCreated, @DateUpdated);

    SELECT *
    FROM Branches
    WHERE BranchID = 
    (
        SELECT TOP 1 BranchID
        FROM @Inserted
    );
END
GO