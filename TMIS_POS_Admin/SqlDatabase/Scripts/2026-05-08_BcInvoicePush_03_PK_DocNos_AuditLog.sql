-- =============================================================
-- Migration : Spec 3 addendum
-- Date      : 2026-05-08
-- Step      : 03 - PK rename + doc numbers + audit log
--
-- Three changes applied to POS_InvoiceHeader_BC + a new audit table:
--
-- 1) Adopt the standard PK naming convention. The table previously
--    used FK_InvoiceHeaderID as PK; introduce InvoiceHeaderBcID
--    UNIQUEIDENTIFIER as the PK and demote FK_InvoiceHeaderID to
--    a UNIQUE constraint (still 1:1 with the parent invoice).
--
-- 2) Capture BC document numbers alongside their GUIDs:
--      BC_SalesOrderNo - BC sales order doc number (e.g. SALESORD10)
--      BC_InvoiceNo    - posted invoice doc number (e.g. PINV00012)
--    The GUIDs (BC_SalesOrderID / BC_InvoiceID) are still kept for
--    API operations; the No. fields are for human use - search,
--    display, navigation in BC UI.
--
-- 3) New POS_InvoiceHeader_BC_AuditLog table that records every
--    push attempt. BC_LastError on the extension table still holds
--    the most-recent error (cheap UI read), but every error is
--    additionally appended here so history is preserved instead of
--    being overwritten on the next attempt.
--
-- Idempotent. Safe to re-run.
-- =============================================================
USE [TMIS_Development];
GO

-- ----- 1. Add InvoiceHeaderBcID column -----------------------
IF NOT EXISTS (SELECT 1 FROM sys.columns
               WHERE object_id = OBJECT_ID(N'[dbo].[POS_InvoiceHeader_BC]')
                 AND name = N'InvoiceHeaderBcID')
BEGIN
    ALTER TABLE [dbo].[POS_InvoiceHeader_BC]
        ADD [InvoiceHeaderBcID] UNIQUEIDENTIFIER NOT NULL
            CONSTRAINT [DF_POS_InvoiceHeader_BC_InvoiceHeaderBcID] DEFAULT NEWID();
END
GO

-- ----- 2. Drop the old PK on FK_InvoiceHeaderID --------------
DECLARE @oldPk SYSNAME;
SELECT @oldPk = kc.name
  FROM sys.key_constraints kc
  JOIN sys.indexes i ON i.object_id = kc.parent_object_id AND i.index_id = kc.unique_index_id
  JOIN sys.index_columns ic ON ic.object_id = i.object_id AND ic.index_id = i.index_id
  JOIN sys.columns c ON c.object_id = ic.object_id AND c.column_id = ic.column_id
 WHERE kc.parent_object_id = OBJECT_ID(N'[dbo].[POS_InvoiceHeader_BC]')
   AND kc.type = 'PK'
   AND c.name = 'FK_InvoiceHeaderID';

IF @oldPk IS NOT NULL
    EXEC ('ALTER TABLE [dbo].[POS_InvoiceHeader_BC] DROP CONSTRAINT [' + @oldPk + ']');
GO

-- ----- 3. Add the new PK on InvoiceHeaderBcID ----------------
IF NOT EXISTS (
    SELECT 1 FROM sys.indexes
    WHERE object_id = OBJECT_ID(N'[dbo].[POS_InvoiceHeader_BC]')
      AND is_primary_key = 1)
BEGIN
    ALTER TABLE [dbo].[POS_InvoiceHeader_BC]
        ADD CONSTRAINT [PK_POS_InvoiceHeader_BC]
            PRIMARY KEY ([InvoiceHeaderBcID]);
END
GO

-- ----- 4. UNIQUE on FK_InvoiceHeaderID (1:1 with parent) -----
IF NOT EXISTS (SELECT 1 FROM sys.indexes
               WHERE object_id = OBJECT_ID(N'[dbo].[POS_InvoiceHeader_BC]')
                 AND name = N'UQ_POS_InvoiceHeader_BC_FK_InvoiceHeaderID')
BEGIN
    ALTER TABLE [dbo].[POS_InvoiceHeader_BC]
        ADD CONSTRAINT [UQ_POS_InvoiceHeader_BC_FK_InvoiceHeaderID]
            UNIQUE ([FK_InvoiceHeaderID]);
END
GO

-- ----- 5. Document number columns ----------------------------
IF NOT EXISTS (SELECT 1 FROM sys.columns
               WHERE object_id = OBJECT_ID(N'[dbo].[POS_InvoiceHeader_BC]')
                 AND name = N'BC_SalesOrderNo')
BEGIN
    ALTER TABLE [dbo].[POS_InvoiceHeader_BC]
        ADD [BC_SalesOrderNo] VARCHAR(50) NULL;
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.columns
               WHERE object_id = OBJECT_ID(N'[dbo].[POS_InvoiceHeader_BC]')
                 AND name = N'BC_InvoiceNo')
BEGIN
    ALTER TABLE [dbo].[POS_InvoiceHeader_BC]
        ADD [BC_InvoiceNo] VARCHAR(50) NULL;
END
GO

-- ----- 6. Audit log table ------------------------------------
IF OBJECT_ID(N'[dbo].[POS_InvoiceHeader_BC_AuditLog]', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[POS_InvoiceHeader_BC_AuditLog]
    (
        [InvoiceHeaderBcAuditLogID] UNIQUEIDENTIFIER NOT NULL
            CONSTRAINT [DF_POS_InvoiceHeader_BC_AuditLog_ID] DEFAULT NEWID()
            CONSTRAINT [PK_POS_InvoiceHeader_BC_AuditLog] PRIMARY KEY,

        [FK_InvoiceHeaderID] UNIQUEIDENTIFIER NOT NULL,

        [AttemptedAt]    DATETIME    NOT NULL
            CONSTRAINT [DF_POS_InvoiceHeader_BC_AuditLog_AttemptedAt] DEFAULT GETDATE(),

        -- Free-text. e.g. "CreateOrder" / "AddLine" / "ShipAndInvoice" /
        -- "Validate" / "Resume" / "Skip" / "OrderOnly".
        [Stage] VARCHAR(50) NULL,

        -- 'Success' | 'Failure'
        [Outcome] VARCHAR(20) NOT NULL,

        [BC_SalesOrderID] VARCHAR(255) NULL,
        [BC_SalesOrderNo] VARCHAR(50)  NULL,
        [BC_InvoiceID]    VARCHAR(255) NULL,
        [BC_InvoiceNo]    VARCHAR(50)  NULL,

        [ErrorMessage]    VARCHAR(MAX) NULL,

        CONSTRAINT [FK_POS_InvoiceHeader_BC_AuditLog_InvoiceHeader]
            FOREIGN KEY ([FK_InvoiceHeaderID])
            REFERENCES [dbo].[POS_InvoiceHeaders] ([InvoiceHeaderID])
    );
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes
               WHERE object_id = OBJECT_ID(N'[dbo].[POS_InvoiceHeader_BC_AuditLog]')
                 AND name = N'IX_POS_InvoiceHeader_BC_AuditLog_FK_InvoiceHeaderID')
BEGIN
    CREATE INDEX [IX_POS_InvoiceHeader_BC_AuditLog_FK_InvoiceHeaderID]
        ON [dbo].[POS_InvoiceHeader_BC_AuditLog] ([FK_InvoiceHeaderID], [AttemptedAt] DESC);
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes
               WHERE object_id = OBJECT_ID(N'[dbo].[POS_InvoiceHeader_BC_AuditLog]')
                 AND name = N'IX_POS_InvoiceHeader_BC_AuditLog_AttemptedAt')
BEGIN
    CREATE INDEX [IX_POS_InvoiceHeader_BC_AuditLog_AttemptedAt]
        ON [dbo].[POS_InvoiceHeader_BC_AuditLog] ([AttemptedAt] DESC);
END
GO
