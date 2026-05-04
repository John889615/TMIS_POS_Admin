USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.Statuses_select_all', 'P') IS NOT NULL
    DROP PROCEDURE dbo.Statuses_select_all;
GO

CREATE PROCEDURE dbo.Statuses_select_all
AS
BEGIN
    SELECT *
    FROM Statuses;
END
GO