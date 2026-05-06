-- =============================================================
-- Migration : FOH-to-Admin Schema Reconciliation
-- Date      : 2026-05-06
-- Step      : 01 of 07 - POS_InvoiceLines
--
-- Aligns POS_InvoiceLines with FOH InvoiceLines:
--   - FK_ProductID INT NOT NULL --> NULL
--   - SyncedToServer BIT NOT NULL --> NULL
--
-- After running this script:
--   1. Drop regenerable CRUD SPs so the generator rebuilds them:
--        DROP PROCEDURE dbo.POS_InvoiceLines_insert;
--        DROP PROCEDURE dbo.POS_InvoiceLines_update;
--   2. Run the code generator. It should regenerate:
--        - InvoiceLine_Base.cs (FK_ProductID, SyncedToServer now nullable)
--        - Sync_Base_Service.cs CRUD wrappers
--        - Sync_Base_Translator.cs
--        - POS_InvoiceLines_insert / _update SPs
--   3. Then move on to script 02.
--
-- This script is idempotent - safe to re-run.
-- =============================================================
USE [TMIS_Development];
GO

-- ----- FK_ProductID NOT NULL --> NULL ------------------------
IF EXISTS (SELECT 1 FROM sys.columns
           WHERE object_id = OBJECT_ID(N'[dbo].[POS_InvoiceLines]')
             AND name = N'FK_ProductID'
             AND is_nullable = 0)
BEGIN
    ALTER TABLE [dbo].[POS_InvoiceLines] ALTER COLUMN [FK_ProductID] INT NULL;
END
GO

-- ----- SyncedToServer NOT NULL --> NULL ----------------------
IF EXISTS (SELECT 1 FROM sys.columns
           WHERE object_id = OBJECT_ID(N'[dbo].[POS_InvoiceLines]')
             AND name = N'SyncedToServer'
             AND is_nullable = 0)
BEGIN
    ALTER TABLE [dbo].[POS_InvoiceLines] ALTER COLUMN [SyncedToServer] BIT NULL;
END
GO
