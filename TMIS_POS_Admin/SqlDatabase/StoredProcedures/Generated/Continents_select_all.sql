USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.Continents_select_all', 'P') IS NOT NULL
    DROP PROCEDURE dbo.Continents_select_all;
GO

CREATE PROCEDURE dbo.Continents_select_all
AS
BEGIN
    SELECT *
    FROM Continents;
END
GO