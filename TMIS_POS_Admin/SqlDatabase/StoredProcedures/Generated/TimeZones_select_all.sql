USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.TimeZones_select_all', 'P') IS NOT NULL
    DROP PROCEDURE dbo.TimeZones_select_all;
GO

CREATE PROCEDURE dbo.TimeZones_select_all
AS
BEGIN
    SELECT *
    FROM TimeZones;
END
GO