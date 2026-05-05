USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_Logs_select_all', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_Logs_select_all;
GO

CREATE PROCEDURE dbo.POS_Logs_select_all
AS
BEGIN
    SELECT *
    FROM POS_Logs;
END
GO