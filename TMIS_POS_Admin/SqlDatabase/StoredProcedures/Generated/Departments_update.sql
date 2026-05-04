USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.Departments_update', 'P') IS NOT NULL
    DROP PROCEDURE dbo.Departments_update;
GO

CREATE PROCEDURE dbo.Departments_update
    @DepartmentID INT,
    @ShortCode VARCHAR(8),
    @Name VARCHAR(255),
    @FK_StatusID INT,
    @DateCreated DATETIME,
    @DateUpdated DATETIME
AS
BEGIN
    UPDATE Departments
    SET     ShortCode = @ShortCode,
    [Name] = @Name,
    FK_StatusID = @FK_StatusID,
    DateUpdated = @DateUpdated
    WHERE DepartmentID = @DepartmentID;

    SELECT *
    FROM Departments
    WHERE DepartmentID = @DepartmentID;
END
GO