-- =============================================================
-- Migration : Push Sync Redesign (Spec 2)
-- Date      : 2026-05-06
-- Step      : 01 of 01 - BatchDedupe table
--
-- Creates the idempotency cache used by POST /api/sync/push/batch
-- on Admin. Each call from FOH carries a unique BatchID; replays of
-- the same BatchID return the cached ResultJson without re-processing.
--
-- 7-day rolling cleanup is owned by dbo.BatchDedupe_cleanup, which
-- should be wired to a SQL Agent job (or Hangfire) running nightly.
--
-- After running this script:
--   1. Deploy the three custom SPs:
--        BatchDedupe_lookup
--        BatchDedupe_complete
--        BatchDedupe_cleanup
--   2. Run the code generator (defensive - no Base.cs needed since
--      this is custom-only, but the generator may discover it).
--   3. Wire BatchDedupe_cleanup to a nightly job.
--   4. Deploy the new SyncController.PushBatch endpoint code.
--
-- This script is idempotent - safe to re-run.
-- =============================================================
USE [TMIS_Development];
GO

-- ----- Create table if missing -------------------------------
IF OBJECT_ID(N'[dbo].[BatchDedupe]', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[BatchDedupe]
    (
        [BatchID]    UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
        [SiteID]     INT              NOT NULL,
        [GroupName]  VARCHAR(20)      NOT NULL,
        [ReceivedAt] DATETIME         NOT NULL CONSTRAINT [DF_BatchDedupe_ReceivedAt] DEFAULT GETDATE(),
        [ResultJson] VARCHAR(MAX)     NOT NULL
    );
END
GO

-- ----- Index for cleanup query (ReceivedAt range scan) -------
IF NOT EXISTS (SELECT 1 FROM sys.indexes
               WHERE object_id = OBJECT_ID(N'[dbo].[BatchDedupe]')
                 AND name = N'IX_BatchDedupe_ReceivedAt')
BEGIN
    CREATE INDEX [IX_BatchDedupe_ReceivedAt]
        ON [dbo].[BatchDedupe] ([ReceivedAt]);
END
GO

-- ----- Index for diagnostic queries by site/group ------------
IF NOT EXISTS (SELECT 1 FROM sys.indexes
               WHERE object_id = OBJECT_ID(N'[dbo].[BatchDedupe]')
                 AND name = N'IX_BatchDedupe_Site_Group')
BEGIN
    CREATE INDEX [IX_BatchDedupe_Site_Group]
        ON [dbo].[BatchDedupe] ([SiteID], [GroupName], [ReceivedAt]);
END
GO
