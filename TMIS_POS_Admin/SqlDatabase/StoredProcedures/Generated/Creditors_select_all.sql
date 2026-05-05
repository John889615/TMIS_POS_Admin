USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.Creditors_select_all', 'P') IS NOT NULL
    DROP PROCEDURE dbo.Creditors_select_all;
GO

CREATE PROCEDURE dbo.Creditors_select_all
AS
BEGIN
    SELECT *
    FROM Creditors;
END
GO