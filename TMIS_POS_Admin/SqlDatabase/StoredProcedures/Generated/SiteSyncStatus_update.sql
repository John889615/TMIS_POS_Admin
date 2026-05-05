USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.SiteSyncStatus_update', 'P') IS NOT NULL
    DROP PROCEDURE dbo.SiteSyncStatus_update;
GO

CREATE PROCEDURE dbo.SiteSyncStatus_update
    @SiteId INT,
    @TypeName NVARCHAR(100),
    @LastSuccessAt DATETIME2 = NULL,
    @LastFailureAt DATETIME2 = NULL,
    @ConsecutiveFailures INT,
    @LastErrorMessage NVARCHAR(2000) = NULL,
    @LastReportedAt DATETIME2,
    @AlertSentAt DATETIME2 = NULL
AS
BEGIN
    UPDATE SiteSyncStatus
    SET     LastSuccessAt = @LastSuccessAt,
    LastFailureAt = @LastFailureAt,
    ConsecutiveFailures = @ConsecutiveFailures,
    LastErrorMessage = @LastErrorMessage,
    LastReportedAt = @LastReportedAt,
    AlertSentAt = @AlertSentAt
    WHERE SiteId = @SiteId
    AND TypeName = @TypeName;

    SELECT *
    FROM SiteSyncStatus
    WHERE SiteId = @SiteId
    AND TypeName = @TypeName;
END
GO