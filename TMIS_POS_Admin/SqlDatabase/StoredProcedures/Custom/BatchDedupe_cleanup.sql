USE [TMIS_Development]
GO

IF OBJECT_ID('dbo.BatchDedupe_cleanup', 'P') IS NOT NULL
    DROP PROCEDURE dbo.BatchDedupe_cleanup;
GO

-- =============================================================
-- BatchDedupe_cleanup
--   Deletes BatchDedupe rows older than @RetentionDays days.
--   Default retention: 7 days.
--
--   Wire to a SQL Agent job (or Hangfire) running nightly:
--     EXEC dbo.BatchDedupe_cleanup;
-- =============================================================
CREATE PROCEDURE dbo.BatchDedupe_cleanup
    @RetentionDays INT = 7
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    IF @RetentionDays IS NULL OR @RetentionDays < 1
        SET @RetentionDays = 7;

    DECLARE @CutoffDate DATETIME = DATEADD(DAY, -@RetentionDays, GETDATE());

    DELETE FROM [dbo].[BatchDedupe]
     WHERE [ReceivedAt] < @CutoffDate;

    SELECT @@ROWCOUNT AS [RowsDeleted];
END
GO
