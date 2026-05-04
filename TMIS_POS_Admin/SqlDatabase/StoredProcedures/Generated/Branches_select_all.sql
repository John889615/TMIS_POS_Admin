USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.Branches_select_all', 'P') IS NOT NULL
    DROP PROCEDURE dbo.Branches_select_all;
GO

CREATE PROCEDURE dbo.Branches_select_all
AS
BEGIN
    SELECT *
    FROM Branches;
END
GO