-- =============================================================
-- Migration : FOH-to-Admin Schema Reconciliation
-- Date      : 2026-05-06
-- Step      : 02 of 07 - POS_InvoiceTabs
--
-- Aligns POS_InvoiceTabs with FOH InvoiceTabs:
--   - SyncedToServer BIT NOT NULL --> NULL
--
-- After running this script:
--   1. Drop regenerable CRUD SPs so the generator rebuilds them:
--        DROP PROCEDURE dbo.POS_InvoiceTabs_insert;
--        DROP PROCEDURE dbo.POS_InvoiceTabs_update;
--   2. Run the code generator.
--   3. Then move on to script 03.
--
-- This script is idempotent - safe to re-run.
-- =============================================================
USE [TMIS_Development];
GO

IF EXISTS (SELECT 1 FROM sys.columns
           WHERE object_id = OBJECT_ID(N'[dbo].[POS_InvoiceTabs]')
             AND name = N'SyncedToServer'
             AND is_nullable = 0)
BEGIN
    ALTER TABLE [dbo].[POS_InvoiceTabs] ALTER COLUMN [SyncedToServer] BIT NULL;
END
GO
