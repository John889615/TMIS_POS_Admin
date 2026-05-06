-- =============================================================
-- Migration : FOH-to-Admin Schema Reconciliation
-- Date      : 2026-05-06
-- Step      : 05 of 07 - POS_TablineSubstitutes
--
-- Aligns POS_TablineSubstitutes with FOH TablineSubstitutes:
--   - Rename PK column POS_TablineSubstituteID --> TablineSubstituteID
--   - ADD FK_ParentTabLineCombinationID UNIQUEIDENTIFIER NULL FK
--         -> POS_TabLineCombinations(TabLineCombinationID)
--
-- After running this script:
--   1. Drop regenerable CRUD SPs so the generator rebuilds them:
--        DROP PROCEDURE dbo.POS_TablineSubstitutes_insert;
--        DROP PROCEDURE dbo.POS_TablineSubstitutes_update;
--   2. Run the code generator.
--   3. Hand-fix any custom SPs that reference the old PK column name
--      POS_TablineSubstituteID -> TablineSubstituteID. Search:
--        TMIS_POS_Admin\SqlDatabase\Stored Procedures\Custom\
--   4. Then move on to script 06.
--
-- This script is idempotent - safe to re-run.
-- =============================================================
USE [TMIS_Development];
GO

-- ----- Rename PK column --------------------------------------
IF EXISTS (SELECT 1 FROM sys.columns
           WHERE object_id = OBJECT_ID(N'[dbo].[POS_TablineSubstitutes]')
             AND name = N'POS_TablineSubstituteID')
BEGIN
    EXEC sp_rename
        @objname = N'[dbo].[POS_TablineSubstitutes].POS_TablineSubstituteID',
        @newname = N'TablineSubstituteID',
        @objtype = N'COLUMN';
END
GO

-- ----- ADD FK_ParentTabLineCombinationID ---------------------
IF NOT EXISTS (SELECT 1 FROM sys.columns
               WHERE object_id = OBJECT_ID(N'[dbo].[POS_TablineSubstitutes]')
                 AND name = N'FK_ParentTabLineCombinationID')
BEGIN
    ALTER TABLE [dbo].[POS_TablineSubstitutes]
        ADD [FK_ParentTabLineCombinationID] UNIQUEIDENTIFIER NULL;
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys
               WHERE parent_object_id = OBJECT_ID(N'[dbo].[POS_TablineSubstitutes]')
                 AND name = N'FK_POS_TablineSubstitutes_ParentCombination')
BEGIN
    ALTER TABLE [dbo].[POS_TablineSubstitutes]
        ADD CONSTRAINT [FK_POS_TablineSubstitutes_ParentCombination]
            FOREIGN KEY ([FK_ParentTabLineCombinationID])
            REFERENCES [dbo].[POS_TabLineCombinations] ([TabLineCombinationID]);
END
GO
