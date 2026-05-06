-- =============================================================
-- Migration : FOH-to-Admin Schema Reconciliation - addendum
-- Date      : 2026-05-06
-- Step      : 12 of 12 - drop vestigial SyncedToServer column
--
-- POS_InvoiceHeaders / POS_InvoiceTabs / POS_InvoiceLines all carried
-- a SyncedToServer column copied from the FOH side. It has no meaning
-- on Admin (Admin IS the server). Some Admin DBs have it; others
-- never did. This script drops it where it exists. The matching
-- BulkUpsertToServer_* SPs no longer reference it.
--
-- Idempotent. Safe to re-run.
-- =============================================================
USE [TMIS_Development];
GO

-- ----- POS_InvoiceHeaders ------------------------------------
IF EXISTS (SELECT 1 FROM sys.columns
           WHERE object_id = OBJECT_ID(N'[dbo].[POS_InvoiceHeaders]')
             AND name = N'SyncedToServer')
BEGIN
    -- Drop any default constraint on the column first
    DECLARE @df1 SYSNAME;
    SELECT @df1 = dc.name
      FROM sys.default_constraints dc
      JOIN sys.columns c ON c.default_object_id = dc.object_id
     WHERE c.object_id = OBJECT_ID(N'[dbo].[POS_InvoiceHeaders]')
       AND c.name = N'SyncedToServer';
    IF @df1 IS NOT NULL
        EXEC ('ALTER TABLE [dbo].[POS_InvoiceHeaders] DROP CONSTRAINT [' + @df1 + ']');

    ALTER TABLE [dbo].[POS_InvoiceHeaders] DROP COLUMN [SyncedToServer];
END
GO

-- ----- POS_InvoiceTabs ---------------------------------------
IF EXISTS (SELECT 1 FROM sys.columns
           WHERE object_id = OBJECT_ID(N'[dbo].[POS_InvoiceTabs]')
             AND name = N'SyncedToServer')
BEGIN
    DECLARE @df2 SYSNAME;
    SELECT @df2 = dc.name
      FROM sys.default_constraints dc
      JOIN sys.columns c ON c.default_object_id = dc.object_id
     WHERE c.object_id = OBJECT_ID(N'[dbo].[POS_InvoiceTabs]')
       AND c.name = N'SyncedToServer';
    IF @df2 IS NOT NULL
        EXEC ('ALTER TABLE [dbo].[POS_InvoiceTabs] DROP CONSTRAINT [' + @df2 + ']');

    ALTER TABLE [dbo].[POS_InvoiceTabs] DROP COLUMN [SyncedToServer];
END
GO

-- ----- POS_InvoiceLines --------------------------------------
IF EXISTS (SELECT 1 FROM sys.columns
           WHERE object_id = OBJECT_ID(N'[dbo].[POS_InvoiceLines]')
             AND name = N'SyncedToServer')
BEGIN
    DECLARE @df3 SYSNAME;
    SELECT @df3 = dc.name
      FROM sys.default_constraints dc
      JOIN sys.columns c ON c.default_object_id = dc.object_id
     WHERE c.object_id = OBJECT_ID(N'[dbo].[POS_InvoiceLines]')
       AND c.name = N'SyncedToServer';
    IF @df3 IS NOT NULL
        EXEC ('ALTER TABLE [dbo].[POS_InvoiceLines] DROP CONSTRAINT [' + @df3 + ']');

    ALTER TABLE [dbo].[POS_InvoiceLines] DROP COLUMN [SyncedToServer];
END
GO
