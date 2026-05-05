USE [TMIS_Development]
GO

IF OBJECT_ID('dbo.Departments_insert', 'P') IS NOT NULL
    DROP PROCEDURE dbo.Departments_insert;
GO

CREATE PROCEDURE dbo.Departments_insert
    @ShortCode VARCHAR(8),
    @Name VARCHAR(255),
    @FK_StatusID INT,
    @DateCreated DATETIME,
    @DateUpdated DATETIME
AS
BEGIN
    DECLARE @Inserted TABLE (DepartmentID INT);

    INSERT INTO Departments (ShortCode, [Name], FK_StatusID, DateCreated, DateUpdated)
    OUTPUT INSERTED.DepartmentID INTO @Inserted
    VALUES (@ShortCode, @Name, @FK_StatusID, @DateCreated, @DateUpdated);

    SELECT *
    FROM Departments
    WHERE DepartmentID = 
    (
        SELECT TOP 1 DepartmentID
        FROM @Inserted
    );
END
GO