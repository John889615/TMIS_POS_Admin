-- =============================================================
-- Migration : FOH-to-Admin Schema Reconciliation
-- Date      : 2026-05-06
-- Step      : 07 of 07 - POS_InvoicePayments  (the big one)
--
-- Aligns POS_InvoicePayments with FOH InvoicePayments. This is the
-- most invasive script: it renames 6 columns, drops 2, adds 9, and
-- tightens nullability on 6.
--
-- Renames (sp_rename, no data move):
--   FK_FromCurrencyID  -> FK_BaseCurrencyID
--   FK_ToCurrencyID    -> FK_PaymentCurrencyID
--   FromCurrency       -> BaseCurrencyCode
--   ToCurrency         -> PaymentCurrencyCode
--   FromAmountPaid     -> BaseAmountPaid
--   ToAmountPaid       -> PaymentAmountPaid
--
-- Drops (no longer in FOH schema):
--   FromTotal
--   ToTotal
--
-- Adds (with backfill where required):
--   StaffName       VARCHAR(255)     NN  (Pattern B: backfill = '?')
--   IdempotencyKey  UNIQUEIDENTIFIER NN  (Pattern B: backfill = NEWID())
--   Reference       VARCHAR(100)     NULL
--   Notes           VARCHAR(MAX)     NULL
--   IsVoided        BIT              NN DEFAULT 0
--   VoidReason      VARCHAR(255)     NULL
--   VoidedDate      DATETIME         NULL
--   VoidedBy        VARCHAR(255)     NULL  (per staff-not-synced rule)
--   SignatureBase64 VARCHAR(MAX)     NULL
--
-- Tightens (NULL -> NOT NULL with backfill):
--   FK_InvoiceID            (PRE-FLIGHT: throws if any row has NULL)
--   FK_BaseCurrencyID       (backfill = 1)
--   FK_PaymentCurrencyID    (backfill = 1)
--   ExchangeRate            (was DECIMAL(18,4) NULL; widen to (18,6) NN, backfill 1.0)
--   ExchangeDate            (backfill = COALESCE(DatePaid, GETDATE()))
--   DatePaid                (backfill = COALESCE(ExchangeDate, GETDATE()))
--
-- WARNING: pre-flight will THROW if any row has NULL FK_InvoiceID.
-- Resolve those rows manually before re-running.
--
-- WARNING: legacy IdempotencyKey values are synthetic NEWID()s, NOT
-- the FOH-supplied keys. Spec 2 (sync redesign) treats InvoicePaymentID
-- as the natural dedupe key for legacy rows; only newly-pushed payments
-- after this migration use IdempotencyKey for dedupe.
--
-- After running this script:
--   1. Drop regenerable CRUD SPs so the generator rebuilds them:
--        DROP PROCEDURE dbo.POS_InvoicePayments_insert;
--        DROP PROCEDURE dbo.POS_InvoicePayments_update;
--   2. Run the code generator. It will regenerate Base/Translator/SPs
--      against the new column names.
--   3. Hand-fix any custom SPs that reference renamed columns:
--        Search for: FK_FromCurrencyID, FK_ToCurrencyID,
--                    FromCurrency, ToCurrency,
--                    FromTotal, ToTotal,
--                    FromAmountPaid, ToAmountPaid
--        Under: TMIS_POS_Admin\SqlDatabase\Stored Procedures\Custom\
--        Two known custom SPs to inspect:
--          BulkUpsertToServer_InvoicePayments.sql
--          (any in CurrencyExchangeRates_* if they cross-ref payments)
--   4. Schema reconciliation is COMPLETE - move to Spec 2 (sync redesign).
--
-- This script is idempotent - safe to re-run.
-- =============================================================
USE [TMIS_Development];
GO

-- ----- Pre-flight: refuse to run if any row has NULL FK_InvoiceID --
IF EXISTS (SELECT 1 FROM [dbo].[POS_InvoicePayments] WHERE FK_InvoiceID IS NULL)
BEGIN
    THROW 50001,
        'POS_InvoicePayments has rows with NULL FK_InvoiceID. Resolve those rows (delete or assign an invoice) before running this migration.',
        1;
END
GO

-- ----- Drop FromTotal, ToTotal -------------------------------
IF EXISTS (SELECT 1 FROM sys.columns
           WHERE object_id = OBJECT_ID(N'[dbo].[POS_InvoicePayments]')
             AND name = N'FromTotal')
BEGIN
    ALTER TABLE [dbo].[POS_InvoicePayments] DROP COLUMN [FromTotal];
END
GO

IF EXISTS (SELECT 1 FROM sys.columns
           WHERE object_id = OBJECT_ID(N'[dbo].[POS_InvoicePayments]')
             AND name = N'ToTotal')
BEGIN
    ALTER TABLE [dbo].[POS_InvoicePayments] DROP COLUMN [ToTotal];
END
GO

-- ----- Rename From/To columns to Base/Payment ----------------
IF EXISTS (SELECT 1 FROM sys.columns
           WHERE object_id = OBJECT_ID(N'[dbo].[POS_InvoicePayments]')
             AND name = N'FK_FromCurrencyID')
BEGIN
    EXEC sp_rename
        @objname = N'[dbo].[POS_InvoicePayments].FK_FromCurrencyID',
        @newname = N'FK_BaseCurrencyID',
        @objtype = N'COLUMN';
END
GO

IF EXISTS (SELECT 1 FROM sys.columns
           WHERE object_id = OBJECT_ID(N'[dbo].[POS_InvoicePayments]')
             AND name = N'FK_ToCurrencyID')
BEGIN
    EXEC sp_rename
        @objname = N'[dbo].[POS_InvoicePayments].FK_ToCurrencyID',
        @newname = N'FK_PaymentCurrencyID',
        @objtype = N'COLUMN';
END
GO

IF EXISTS (SELECT 1 FROM sys.columns
           WHERE object_id = OBJECT_ID(N'[dbo].[POS_InvoicePayments]')
             AND name = N'FromCurrency')
BEGIN
    EXEC sp_rename
        @objname = N'[dbo].[POS_InvoicePayments].FromCurrency',
        @newname = N'BaseCurrencyCode',
        @objtype = N'COLUMN';
END
GO

IF EXISTS (SELECT 1 FROM sys.columns
           WHERE object_id = OBJECT_ID(N'[dbo].[POS_InvoicePayments]')
             AND name = N'ToCurrency')
BEGIN
    EXEC sp_rename
        @objname = N'[dbo].[POS_InvoicePayments].ToCurrency',
        @newname = N'PaymentCurrencyCode',
        @objtype = N'COLUMN';
END
GO

IF EXISTS (SELECT 1 FROM sys.columns
           WHERE object_id = OBJECT_ID(N'[dbo].[POS_InvoicePayments]')
             AND name = N'FromAmountPaid')
BEGIN
    EXEC sp_rename
        @objname = N'[dbo].[POS_InvoicePayments].FromAmountPaid',
        @newname = N'BaseAmountPaid',
        @objtype = N'COLUMN';
END
GO

IF EXISTS (SELECT 1 FROM sys.columns
           WHERE object_id = OBJECT_ID(N'[dbo].[POS_InvoicePayments]')
             AND name = N'ToAmountPaid')
BEGIN
    EXEC sp_rename
        @objname = N'[dbo].[POS_InvoicePayments].ToAmountPaid',
        @newname = N'PaymentAmountPaid',
        @objtype = N'COLUMN';
END
GO

-- ----- Backfill nullable columns before tightening -----------
EXEC sp_executesql N'
    UPDATE [dbo].[POS_InvoicePayments] SET ExchangeRate         = 1                               WHERE ExchangeRate         IS NULL;
    UPDATE [dbo].[POS_InvoicePayments] SET ExchangeDate         = COALESCE(DatePaid, GETDATE())   WHERE ExchangeDate         IS NULL;
    UPDATE [dbo].[POS_InvoicePayments] SET DatePaid             = COALESCE(ExchangeDate, GETDATE()) WHERE DatePaid           IS NULL;
    UPDATE [dbo].[POS_InvoicePayments] SET FK_BaseCurrencyID    = 1                               WHERE FK_BaseCurrencyID    IS NULL;
    UPDATE [dbo].[POS_InvoicePayments] SET FK_PaymentCurrencyID = 1                               WHERE FK_PaymentCurrencyID IS NULL;
';
GO

-- ----- Tighten nullability + widen ExchangeRate precision ----
IF EXISTS (SELECT 1 FROM sys.columns
           WHERE object_id = OBJECT_ID(N'[dbo].[POS_InvoicePayments]')
             AND name = N'FK_InvoiceID' AND is_nullable = 1)
    ALTER TABLE [dbo].[POS_InvoicePayments] ALTER COLUMN [FK_InvoiceID] UNIQUEIDENTIFIER NOT NULL;
GO

IF EXISTS (SELECT 1 FROM sys.columns
           WHERE object_id = OBJECT_ID(N'[dbo].[POS_InvoicePayments]')
             AND name = N'FK_BaseCurrencyID' AND is_nullable = 1)
    ALTER TABLE [dbo].[POS_InvoicePayments] ALTER COLUMN [FK_BaseCurrencyID] INT NOT NULL;
GO

IF EXISTS (SELECT 1 FROM sys.columns
           WHERE object_id = OBJECT_ID(N'[dbo].[POS_InvoicePayments]')
             AND name = N'FK_PaymentCurrencyID' AND is_nullable = 1)
    ALTER TABLE [dbo].[POS_InvoicePayments] ALTER COLUMN [FK_PaymentCurrencyID] INT NOT NULL;
GO

IF EXISTS (SELECT 1 FROM sys.columns c
           JOIN sys.types t ON t.user_type_id = c.user_type_id
           WHERE c.object_id = OBJECT_ID(N'[dbo].[POS_InvoicePayments]')
             AND c.name = N'ExchangeRate'
             AND (c.is_nullable = 1 OR c.scale <> 6))
    ALTER TABLE [dbo].[POS_InvoicePayments] ALTER COLUMN [ExchangeRate] DECIMAL(18,6) NOT NULL;
GO

IF EXISTS (SELECT 1 FROM sys.columns
           WHERE object_id = OBJECT_ID(N'[dbo].[POS_InvoicePayments]')
             AND name = N'ExchangeDate' AND is_nullable = 1)
    ALTER TABLE [dbo].[POS_InvoicePayments] ALTER COLUMN [ExchangeDate] DATETIME NOT NULL;
GO

IF EXISTS (SELECT 1 FROM sys.columns
           WHERE object_id = OBJECT_ID(N'[dbo].[POS_InvoicePayments]')
             AND name = N'DatePaid' AND is_nullable = 1)
    ALTER TABLE [dbo].[POS_InvoicePayments] ALTER COLUMN [DatePaid] DATETIME NOT NULL;
GO

-- ----- ADD StaffName VARCHAR(255) NN (Pattern B) -------------
IF NOT EXISTS (SELECT 1 FROM sys.columns
               WHERE object_id = OBJECT_ID(N'[dbo].[POS_InvoicePayments]')
                 AND name = N'StaffName')
BEGIN
    ALTER TABLE [dbo].[POS_InvoicePayments] ADD [StaffName] VARCHAR(255) NULL;
END
GO

EXEC sp_executesql N'
    UPDATE [dbo].[POS_InvoicePayments] SET StaffName = ''?'' WHERE StaffName IS NULL;
';
GO

IF EXISTS (SELECT 1 FROM sys.columns
           WHERE object_id = OBJECT_ID(N'[dbo].[POS_InvoicePayments]')
             AND name = N'StaffName' AND is_nullable = 1)
    ALTER TABLE [dbo].[POS_InvoicePayments] ALTER COLUMN [StaffName] VARCHAR(255) NOT NULL;
GO

-- ----- ADD IdempotencyKey UNIQUEIDENTIFIER NN (Pattern B) ----
IF NOT EXISTS (SELECT 1 FROM sys.columns
               WHERE object_id = OBJECT_ID(N'[dbo].[POS_InvoicePayments]')
                 AND name = N'IdempotencyKey')
BEGIN
    ALTER TABLE [dbo].[POS_InvoicePayments] ADD [IdempotencyKey] UNIQUEIDENTIFIER NULL;
END
GO

EXEC sp_executesql N'
    UPDATE [dbo].[POS_InvoicePayments] SET IdempotencyKey = NEWID() WHERE IdempotencyKey IS NULL;
';
GO

IF EXISTS (SELECT 1 FROM sys.columns
           WHERE object_id = OBJECT_ID(N'[dbo].[POS_InvoicePayments]')
             AND name = N'IdempotencyKey' AND is_nullable = 1)
    ALTER TABLE [dbo].[POS_InvoicePayments] ALTER COLUMN [IdempotencyKey] UNIQUEIDENTIFIER NOT NULL;
GO

-- ----- ADD remaining columns (Pattern A / nullable) ----------
IF NOT EXISTS (SELECT 1 FROM sys.columns
               WHERE object_id = OBJECT_ID(N'[dbo].[POS_InvoicePayments]')
                 AND name = N'Reference')
    ALTER TABLE [dbo].[POS_InvoicePayments] ADD [Reference] VARCHAR(100) NULL;
GO

IF NOT EXISTS (SELECT 1 FROM sys.columns
               WHERE object_id = OBJECT_ID(N'[dbo].[POS_InvoicePayments]')
                 AND name = N'Notes')
    ALTER TABLE [dbo].[POS_InvoicePayments] ADD [Notes] VARCHAR(MAX) NULL;
GO

IF NOT EXISTS (SELECT 1 FROM sys.columns
               WHERE object_id = OBJECT_ID(N'[dbo].[POS_InvoicePayments]')
                 AND name = N'IsVoided')
    ALTER TABLE [dbo].[POS_InvoicePayments]
        ADD [IsVoided] BIT NOT NULL CONSTRAINT [DF_POS_InvoicePayments_IsVoided] DEFAULT 0;
GO

IF NOT EXISTS (SELECT 1 FROM sys.columns
               WHERE object_id = OBJECT_ID(N'[dbo].[POS_InvoicePayments]')
                 AND name = N'VoidReason')
    ALTER TABLE [dbo].[POS_InvoicePayments] ADD [VoidReason] VARCHAR(255) NULL;
GO

IF NOT EXISTS (SELECT 1 FROM sys.columns
               WHERE object_id = OBJECT_ID(N'[dbo].[POS_InvoicePayments]')
                 AND name = N'VoidedDate')
    ALTER TABLE [dbo].[POS_InvoicePayments] ADD [VoidedDate] DATETIME NULL;
GO

IF NOT EXISTS (SELECT 1 FROM sys.columns
               WHERE object_id = OBJECT_ID(N'[dbo].[POS_InvoicePayments]')
                 AND name = N'VoidedBy')
    ALTER TABLE [dbo].[POS_InvoicePayments] ADD [VoidedBy] VARCHAR(255) NULL;
GO

IF NOT EXISTS (SELECT 1 FROM sys.columns
               WHERE object_id = OBJECT_ID(N'[dbo].[POS_InvoicePayments]')
                 AND name = N'SignatureBase64')
    ALTER TABLE [dbo].[POS_InvoicePayments] ADD [SignatureBase64] VARCHAR(MAX) NULL;
GO
