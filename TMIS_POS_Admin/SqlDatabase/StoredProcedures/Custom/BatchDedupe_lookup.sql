USE [TMIS_Development]
GO

IF OBJECT_ID('dbo.BatchDedupe_lookup', 'P') IS NOT NULL
    DROP PROCEDURE dbo.BatchDedupe_lookup;
GO

-- =============================================================
-- BatchDedupe_lookup
--   Returns the cached ResultJson for a BatchID, or NULL if no
--   cached result exists. Called by SyncController.PushBatch
--   before processing to short-circuit on idempotent retries.
-- =============================================================
CREATE PROCEDURE dbo.BatchDedupe_lookup
    @BatchID UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT TOP 1
        [BatchID],
        [SiteID],
        [GroupName],
        [ReceivedAt],
        [ResultJson]
      FROM [dbo].[BatchDedupe]
     WHERE [BatchID] = @BatchID;
END
GO
