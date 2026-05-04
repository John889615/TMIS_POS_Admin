USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.EntitySettings_select_all', 'P') IS NOT NULL
    DROP PROCEDURE dbo.EntitySettings_select_all;
GO

CREATE PROCEDURE dbo.EntitySettings_select_all
AS
BEGIN
    SELECT *
    FROM EntitySettings;
END
GO