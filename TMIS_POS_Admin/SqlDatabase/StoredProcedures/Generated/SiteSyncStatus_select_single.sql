USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.SiteSyncStatus_select_single', 'P') IS NOT NULL
    DROP PROCEDURE dbo.SiteSyncStatus_select_single;
GO

CREATE PROCEDURE dbo.SiteSyncStatus_select_single
    @SiteId INT,
    @TypeName NVARCHAR(100)
AS
BEGIN
    SELECT *
    FROM SiteSyncStatus
    WHERE SiteId = @SiteId
    AND TypeName = @TypeName;
END
GO