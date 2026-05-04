USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.Departments_select_single', 'P') IS NOT NULL
    DROP PROCEDURE dbo.Departments_select_single;
GO

CREATE PROCEDURE dbo.Departments_select_single
    @DepartmentID INT
AS
BEGIN
    SELECT *
    FROM Departments
    WHERE DepartmentID = @DepartmentID;
END
GO