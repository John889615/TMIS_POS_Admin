USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.Countries_select_all', 'P') IS NOT NULL
    DROP PROCEDURE dbo.Countries_select_all;
GO

CREATE PROCEDURE dbo.Countries_select_all
AS
BEGIN
    SELECT *
    FROM Countries;
END
GO