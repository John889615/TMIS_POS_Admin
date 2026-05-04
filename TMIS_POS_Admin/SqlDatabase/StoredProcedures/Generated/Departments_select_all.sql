USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.Departments_select_all', 'P') IS NOT NULL
    DROP PROCEDURE dbo.Departments_select_all;
GO

CREATE PROCEDURE dbo.Departments_select_all
AS
BEGIN
    SELECT *
    FROM Departments;
END
GO