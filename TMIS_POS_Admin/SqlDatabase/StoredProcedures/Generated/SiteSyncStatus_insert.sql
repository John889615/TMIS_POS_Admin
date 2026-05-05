USE [TMIS_Development]
GO

IF OBJECT_ID('dbo.SiteSyncStatus_insert', 'P') IS NOT NULL
    DROP PROCEDURE dbo.SiteSyncStatus_insert;
GO

CREATE PROCEDURE dbo.SiteSyncStatus_insert
    @LastSuccessAt DATETIME2 = NULL,
    @LastFailureAt DATETIME2 = NULL,
    @ConsecutiveFailures INT,
    @LastErrorMessage NVARCHAR(2000) = NULL,
    @LastReportedAt DATETIME2,
    @AlertSentAt DATETIME2 = NULL
AS
BEGIN
    DECLARE @Inserted TABLE (SiteId INT);

    INSERT INTO SiteSyncStatus (LastSuccessAt, LastFailureAt, ConsecutiveFailures, LastErrorMessage, LastReportedAt, AlertSentAt)
    OUTPUT INSERTED.SiteId INTO @Inserted
    VALUES (@LastSuccessAt, @LastFailureAt, @ConsecutiveFailures, @LastErrorMessage, @LastReportedAt, @AlertSentAt);

    SELECT *
    FROM SiteSyncStatus
    WHERE SiteId = 
    (
        SELECT TOP 1 SiteId
        FROM @Inserted
    );
END
GO