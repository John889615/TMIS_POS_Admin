-- =============================================================
-- Migration : Add POS_TabLines.FK_CostCenterID
-- Date      : 2026-05-08
--
-- Mirrors the matching FOH-side column on dbo.TabLines
-- (POS_Client_Site/SqlDatabase/Migrations/2026-05-08-add-tablines-costcenter.sql).
-- Each tab line records which cost centre it was sold from so the
-- Business Central push can attribute per-line stock to the right
-- cost centre. Optional - existing rows are left NULL; new inserts
-- get the value from the FOH station's selected cost centre,
-- carried up via the FOH->Admin push sync.
--
-- Idempotent. Safe to re-run.
-- =============================================================
USE [TMIS_Development];
GO

-- ----- Column ------------------------------------------------
IF NOT EXISTS (SELECT 1 FROM sys.columns
               WHERE object_id = OBJECT_ID(N'[dbo].[POS_TabLines]')
                 AND name = N'FK_CostCenterID')
BEGIN
    ALTER TABLE [dbo].[POS_TabLines]
        ADD [FK_CostCenterID] INT NULL;
END
GO

-- ----- Foreign key -------------------------------------------
IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys
               WHERE name = N'FK_POS_TabLines_POS_CostCenters'
                 AND parent_object_id = OBJECT_ID(N'[dbo].[POS_TabLines]'))
BEGIN
    ALTER TABLE [dbo].[POS_TabLines] WITH CHECK
        ADD CONSTRAINT [FK_POS_TabLines_POS_CostCenters]
        FOREIGN KEY ([FK_CostCenterID])
        REFERENCES [dbo].[POS_CostCenters] ([CostCenterID]);
END
GO

-- ----- Supporting index --------------------------------------
IF NOT EXISTS (SELECT 1 FROM sys.indexes
               WHERE name = N'IX_POS_TabLines_FK_CostCenterID'
                 AND object_id = OBJECT_ID(N'[dbo].[POS_TabLines]'))
BEGIN
    CREATE INDEX [IX_POS_TabLines_FK_CostCenterID]
        ON [dbo].[POS_TabLines] ([FK_CostCenterID]);
END
GO
