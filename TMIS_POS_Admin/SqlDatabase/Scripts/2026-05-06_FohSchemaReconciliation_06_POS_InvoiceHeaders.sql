-- =============================================================
-- Migration : FOH-to-Admin Schema Reconciliation
-- Date      : 2026-05-06
-- Step      : 06 of 07 - POS_InvoiceHeaders
--
-- Aligns POS_InvoiceHeaders with FOH InvoiceHeaders:
--   - Widen PartyName        VARCHAR(50)  -> VARCHAR(100)
--   - Widen BookingReference VARCHAR(50)  -> VARCHAR(100)
--   - ADD FK_CurrencyID INT NN FK -> Currencies(CurrencyID)
--         (added NULL, backfilled from any payment's FK_FromCurrencyID,
--          fallback CurrencyID = 1, then tightened to NN)
--   - ADD IsPaid BIT NN DEFAULT 0
--   - ADD AmountPaid DECIMAL(18,4) NN DEFAULT 0
--   - ADD AmountDue  DECIMAL(18,4) NN DEFAULT 0  (backfilled = InclTotal)
--   - ADD IsVoided   BIT NN DEFAULT 0  (backfilled from IsDiscarded)
--   - ADD VoidReason VARCHAR(255) NULL
--   - ADD VoidedDate DATETIME NULL
--   - ADD VoidedBy   VARCHAR(255) NULL
--   - DROP IsDiscarded (and its default constraint)
--   - Loosen SyncedToServer BIT NN -> NULL
--   - KEEP BC_InvoiceID (Admin-only, never appears on FOH)
--
-- WARNING: the FK_CurrencyID backfill defaults legacy invoices with no
-- payment history to CurrencyID = 1. If row 1 of Currencies is NOT the
-- canonical base currency for your deployment, edit the literal '1'
-- inside this script before running.
--
-- After running this script:
--   1. Drop regenerable CRUD SPs so the generator rebuilds them:
--        DROP PROCEDURE dbo.POS_InvoiceHeaders_insert;
--        DROP PROCEDURE dbo.POS_InvoiceHeaders_update;
--   2. Run the code generator.
--   3. Hand-fix any custom SPs that still reference IsDiscarded:
--        Search: TMIS_POS_Admin\SqlDatabase\Stored Procedures\Custom\
--   4. Then move on to script 07 (the big one).
--
-- This script is idempotent - safe to re-run. The IsDiscarded -> IsVoided
-- migration only runs once because IsDiscarded is dropped at the end.
-- =============================================================
USE [TMIS_Development];
GO

-- ----- Widen PartyName 50 -> 100 -----------------------------
IF EXISTS (SELECT 1 FROM sys.columns
           WHERE object_id = OBJECT_ID(N'[dbo].[POS_InvoiceHeaders]')
             AND name = N'PartyName'
             AND max_length = 50)
BEGIN
    ALTER TABLE [dbo].[POS_InvoiceHeaders] ALTER COLUMN [PartyName] VARCHAR(100) NULL;
END
GO

-- ----- Widen BookingReference 50 -> 100 ----------------------
IF EXISTS (SELECT 1 FROM sys.columns
           WHERE object_id = OBJECT_ID(N'[dbo].[POS_InvoiceHeaders]')
             AND name = N'BookingReference'
             AND max_length = 50)
BEGIN
    ALTER TABLE [dbo].[POS_InvoiceHeaders] ALTER COLUMN [BookingReference] VARCHAR(100) NULL;
END
GO

-- ----- ADD FK_CurrencyID (Pattern E - backfill then tighten) -
IF NOT EXISTS (SELECT 1 FROM sys.columns
               WHERE object_id = OBJECT_ID(N'[dbo].[POS_InvoiceHeaders]')
                 AND name = N'FK_CurrencyID')
BEGIN
    ALTER TABLE [dbo].[POS_InvoiceHeaders] ADD [FK_CurrencyID] INT NULL;
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys
               WHERE parent_object_id = OBJECT_ID(N'[dbo].[POS_InvoiceHeaders]')
                 AND name = N'FK_POS_InvoiceHeaders_Currency')
BEGIN
    ALTER TABLE [dbo].[POS_InvoiceHeaders]
        ADD CONSTRAINT [FK_POS_InvoiceHeaders_Currency]
            FOREIGN KEY ([FK_CurrencyID]) REFERENCES [dbo].[Currencies] ([CurrencyID]);
END
GO

-- Backfill: take the from-currency of any payment for this invoice;
-- otherwise fall back to CurrencyID = 1 (edit if not your base).
EXEC sp_executesql N'
    UPDATE H
       SET FK_CurrencyID = COALESCE(
            (SELECT TOP 1 P.FK_FromCurrencyID
               FROM [dbo].[POS_InvoicePayments] P
              WHERE P.FK_InvoiceID = H.InvoiceHeaderID
                AND P.FK_FromCurrencyID IS NOT NULL),
            1)
      FROM [dbo].[POS_InvoiceHeaders] H
     WHERE H.FK_CurrencyID IS NULL;
';
GO

IF EXISTS (SELECT 1 FROM sys.columns
           WHERE object_id = OBJECT_ID(N'[dbo].[POS_InvoiceHeaders]')
             AND name = N'FK_CurrencyID'
             AND is_nullable = 1)
BEGIN
    ALTER TABLE [dbo].[POS_InvoiceHeaders] ALTER COLUMN [FK_CurrencyID] INT NOT NULL;
END
GO

-- ----- ADD IsPaid, AmountPaid, AmountDue ---------------------
IF NOT EXISTS (SELECT 1 FROM sys.columns
               WHERE object_id = OBJECT_ID(N'[dbo].[POS_InvoiceHeaders]')
                 AND name = N'IsPaid')
BEGIN
    ALTER TABLE [dbo].[POS_InvoiceHeaders]
        ADD [IsPaid] BIT NOT NULL CONSTRAINT [DF_POS_InvoiceHeaders_IsPaid] DEFAULT 0;
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.columns
               WHERE object_id = OBJECT_ID(N'[dbo].[POS_InvoiceHeaders]')
                 AND name = N'AmountPaid')
BEGIN
    ALTER TABLE [dbo].[POS_InvoiceHeaders]
        ADD [AmountPaid] DECIMAL(18,4) NOT NULL CONSTRAINT [DF_POS_InvoiceHeaders_AmountPaid] DEFAULT 0;
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.columns
               WHERE object_id = OBJECT_ID(N'[dbo].[POS_InvoiceHeaders]')
                 AND name = N'AmountDue')
BEGIN
    ALTER TABLE [dbo].[POS_InvoiceHeaders]
        ADD [AmountDue] DECIMAL(18,4) NOT NULL CONSTRAINT [DF_POS_InvoiceHeaders_AmountDue] DEFAULT 0;
END
GO

-- Backfill AmountDue = InclTotal for legacy rows (no payment history known).
EXEC sp_executesql N'
    UPDATE [dbo].[POS_InvoiceHeaders]
       SET AmountDue = InclTotal
     WHERE AmountDue = 0
       AND InclTotal > 0;
';
GO

-- ----- ADD IsVoided + audit columns --------------------------
IF NOT EXISTS (SELECT 1 FROM sys.columns
               WHERE object_id = OBJECT_ID(N'[dbo].[POS_InvoiceHeaders]')
                 AND name = N'IsVoided')
BEGIN
    ALTER TABLE [dbo].[POS_InvoiceHeaders]
        ADD [IsVoided] BIT NOT NULL CONSTRAINT [DF_POS_InvoiceHeaders_IsVoided] DEFAULT 0;
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.columns
               WHERE object_id = OBJECT_ID(N'[dbo].[POS_InvoiceHeaders]')
                 AND name = N'VoidReason')
BEGIN
    ALTER TABLE [dbo].[POS_InvoiceHeaders] ADD [VoidReason] VARCHAR(255) NULL;
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.columns
               WHERE object_id = OBJECT_ID(N'[dbo].[POS_InvoiceHeaders]')
                 AND name = N'VoidedDate')
BEGIN
    ALTER TABLE [dbo].[POS_InvoiceHeaders] ADD [VoidedDate] DATETIME NULL;
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.columns
               WHERE object_id = OBJECT_ID(N'[dbo].[POS_InvoiceHeaders]')
                 AND name = N'VoidedBy')
BEGIN
    ALTER TABLE [dbo].[POS_InvoiceHeaders] ADD [VoidedBy] VARCHAR(255) NULL;
END
GO

-- ----- Migrate IsDiscarded -> IsVoided + drop IsDiscarded ----
IF EXISTS (SELECT 1 FROM sys.columns
           WHERE object_id = OBJECT_ID(N'[dbo].[POS_InvoiceHeaders]')
             AND name = N'IsDiscarded')
BEGIN
    -- Carry over rows that were soft-deleted under the old flag.
    EXEC sp_executesql N'
        UPDATE [dbo].[POS_InvoiceHeaders]
           SET IsVoided = 1
         WHERE IsDiscarded = 1
           AND IsVoided = 0;
    ';

    -- Drop any default constraint on IsDiscarded (named or system-generated).
    DECLARE @df_name SYSNAME;
    SELECT @df_name = dc.name
      FROM sys.default_constraints dc
      JOIN sys.columns c
        ON c.default_object_id = dc.object_id
     WHERE c.object_id = OBJECT_ID(N'[dbo].[POS_InvoiceHeaders]')
       AND c.name = N'IsDiscarded';
    IF @df_name IS NOT NULL
        EXEC ('ALTER TABLE [dbo].[POS_InvoiceHeaders] DROP CONSTRAINT [' + @df_name + ']');

    ALTER TABLE [dbo].[POS_InvoiceHeaders] DROP COLUMN [IsDiscarded];
END
GO

-- ----- Loosen SyncedToServer NN -> NULL ----------------------
IF EXISTS (SELECT 1 FROM sys.columns
           WHERE object_id = OBJECT_ID(N'[dbo].[POS_InvoiceHeaders]')
             AND name = N'SyncedToServer'
             AND is_nullable = 0)
BEGIN
    ALTER TABLE [dbo].[POS_InvoiceHeaders] ALTER COLUMN [SyncedToServer] BIT NULL;
END
GO
