USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.SiteSyncStatus_select_all', 'P') IS NOT NULL
    DROP PROCEDURE dbo.SiteSyncStatus_select_all;
GO

CREATE PROCEDURE dbo.SiteSyncStatus_select_all
AS
BEGIN
    SELECT *
    FROM SiteSyncStatus;
END
GO