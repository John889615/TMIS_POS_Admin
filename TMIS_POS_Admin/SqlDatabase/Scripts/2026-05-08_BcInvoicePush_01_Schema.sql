-- =============================================================
-- Migration : Spec 3 - POS to BC invoice push
-- Date      : 2026-05-08
-- Step      : 01 of 01 - schema
--
-- Creates the POS_InvoiceHeader_BC extension table that holds all
-- BC push tracking state. Migrates the existing
-- POS_InvoiceHeaders.BC_InvoiceID column into the new table and
-- drops it from POS_InvoiceHeaders.
--
-- After running this script:
--   1. Drop regenerable POS_InvoiceHeaders SPs so the generator
--      rebuilds them without the BC_InvoiceID column:
--        DROP PROCEDURE dbo.POS_InvoiceHeaders_insert;
--        DROP PROCEDURE dbo.POS_InvoiceHeaders_update;
--   2. Run the code generator. It will regenerate:
--        - InvoiceHeader_Base.cs (BC_InvoiceID property removed)
--        - Sync_Base_Translator.cs (no BC_InvoiceID read)
--        - Sync_Base_Service.cs (no BC_InvoiceID parameter)
--        - POS_InvoiceHeaders_insert / _update SPs
--      Plus pick up the new POS_InvoiceHeader_BC table.
--   3. Deploy the three custom SPs in Custom/ then deploy the new
--      C# (Bc_Push_Service, BcPushHostedService, controller endpoints).
--
-- This script is idempotent - safe to re-run.
-- =============================================================
USE [TMIS_Development];
GO

-- ----- Create the extension table ----------------------------
IF OBJECT_ID(N'[dbo].[POS_InvoiceHeader_BC]', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[POS_InvoiceHeader_BC]
    (
        [FK_InvoiceHeaderID] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
        [BC_InvoiceID]       VARCHAR(255)     NULL,
        [BC_PushedAt]        DATETIME         NULL,
        [BC_LastError]       VARCHAR(MAX)     NULL,
        [BC_LastAttemptAt]   DATETIME         NULL,

        CONSTRAINT [FK_POS_InvoiceHeader_BC_InvoiceHeader]
            FOREIGN KEY ([FK_InvoiceHeaderID])
            REFERENCES [dbo].[POS_InvoiceHeaders] ([InvoiceHeaderID])
    );
END
GO

-- ----- Indexes -----------------------------------------------
IF NOT EXISTS (SELECT 1 FROM sys.indexes
               WHERE object_id = OBJECT_ID(N'[dbo].[POS_InvoiceHeader_BC]')
                 AND name = N'IX_POS_InvoiceHeader_BC_BCInvoiceID')
BEGIN
    CREATE INDEX [IX_POS_InvoiceHeader_BC_BCInvoiceID]
        ON [dbo].[POS_InvoiceHeader_BC] ([BC_InvoiceID])
        WHERE [BC_InvoiceID] IS NOT NULL;
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes
               WHERE object_id = OBJECT_ID(N'[dbo].[POS_InvoiceHeader_BC]')
                 AND name = N'IX_POS_InvoiceHeader_BC_LastAttemptAt')
BEGIN
    CREATE INDEX [IX_POS_InvoiceHeader_BC_LastAttemptAt]
        ON [dbo].[POS_InvoiceHeader_BC] ([BC_LastAttemptAt]);
END
GO

-- ----- Migrate existing BC_InvoiceID values ------------------
-- Only runs while the legacy column still exists. Idempotent: rows
-- already present in the extension table are not overwritten.
IF EXISTS (SELECT 1 FROM sys.columns
           WHERE object_id = OBJECT_ID(N'[dbo].[POS_InvoiceHeaders]')
             AND name = N'BC_InvoiceID')
BEGIN
    EXEC sp_executesql N'
        INSERT INTO [dbo].[POS_InvoiceHeader_BC]
                (FK_InvoiceHeaderID, BC_InvoiceID, BC_PushedAt)
        SELECT  ih.InvoiceHeaderID, ih.BC_InvoiceID, ih.DateCreated
          FROM  [dbo].[POS_InvoiceHeaders] ih
         WHERE  ih.BC_InvoiceID IS NOT NULL
           AND  NOT EXISTS (
                  SELECT 1 FROM [dbo].[POS_InvoiceHeader_BC] x
                  WHERE x.FK_InvoiceHeaderID = ih.InvoiceHeaderID);
    ';
END
GO

-- ----- Drop the legacy column on POS_InvoiceHeaders ----------
IF EXISTS (SELECT 1 FROM sys.columns
           WHERE object_id = OBJECT_ID(N'[dbo].[POS_InvoiceHeaders]')
             AND name = N'BC_InvoiceID')
BEGIN
    -- Drop any default constraint on the column first
    DECLARE @df SYSNAME;
    SELECT @df = dc.name
      FROM sys.default_constraints dc
      JOIN sys.columns c ON c.default_object_id = dc.object_id
     WHERE c.object_id = OBJECT_ID(N'[dbo].[POS_InvoiceHeaders]')
       AND c.name = N'BC_InvoiceID';
    IF @df IS NOT NULL
        EXEC ('ALTER TABLE [dbo].[POS_InvoiceHeaders] DROP CONSTRAINT [' + @df + ']');

    ALTER TABLE [dbo].[POS_InvoiceHeaders] DROP COLUMN [BC_InvoiceID];
END
GO
