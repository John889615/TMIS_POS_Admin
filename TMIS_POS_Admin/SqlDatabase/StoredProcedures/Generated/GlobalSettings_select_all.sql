USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.GlobalSettings_select_all', 'P') IS NOT NULL
    DROP PROCEDURE dbo.GlobalSettings_select_all;
GO

CREATE PROCEDURE dbo.GlobalSettings_select_all
AS
BEGIN
    SELECT *
    FROM GlobalSettings;
END
GO