USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.StatusGroups_select_all', 'P') IS NOT NULL
    DROP PROCEDURE dbo.StatusGroups_select_all;
GO

CREATE PROCEDURE dbo.StatusGroups_select_all
AS
BEGIN
    SELECT *
    FROM StatusGroups;
END
GO