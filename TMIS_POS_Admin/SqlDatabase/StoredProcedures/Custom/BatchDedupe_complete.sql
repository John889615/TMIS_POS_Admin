USE [TMIS_Development]
GO

IF OBJECT_ID('dbo.BatchDedupe_complete', 'P') IS NOT NULL
    DROP PROCEDURE dbo.BatchDedupe_complete;
GO

-- =============================================================
-- BatchDedupe_complete
--   Records a completed batch's result so subsequent retries
--   short-circuit via BatchDedupe_lookup. Idempotent: if the
--   BatchID is already present, this is a no-op (the existing
--   row is the canonical result).
-- =============================================================
CREATE PROCEDURE dbo.BatchDedupe_complete
    @BatchID    UNIQUEIDENTIFIER,
    @SiteID     INT,
    @GroupName  VARCHAR(20),
    @ResultJson VARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    IF @BatchID IS NULL
        THROW 50001, 'BatchDedupe_complete: @BatchID is required.', 1;

    IF @ResultJson IS NULL
        THROW 50002, 'BatchDedupe_complete: @ResultJson is required.', 1;

    -- Idempotent insert: if a row with this BatchID already exists,
    -- leave it alone (PK violation would otherwise throw).
    IF NOT EXISTS (SELECT 1 FROM [dbo].[BatchDedupe] WHERE [BatchID] = @BatchID)
    BEGIN
        INSERT INTO [dbo].[BatchDedupe]
                ([BatchID], [SiteID], [GroupName], [ReceivedAt], [ResultJson])
        VALUES  (@BatchID,  @SiteID,  @GroupName,  GETDATE(),    @ResultJson);
    END
END
GO
