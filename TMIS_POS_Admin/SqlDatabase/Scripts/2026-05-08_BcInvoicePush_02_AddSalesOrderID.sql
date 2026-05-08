-- =============================================================
-- Migration : Spec 3 addendum - resumable BC push
-- Date      : 2026-05-08
-- Step      : 02 of 02 - add BC_SalesOrderID column
--
-- Adds POS_InvoiceHeader_BC.BC_SalesOrderID so a failed
-- shipAndInvoice no longer forces the next attempt to recreate
-- the BC sales order. The order id is stamped immediately after
-- the order header is created in BC; on retry, Bc_Push_Service
-- skips order + line creation and just re-runs shipAndInvoice.
--
-- Idempotent. Safe to re-run.
-- =============================================================
USE [TMIS_Development];
GO

IF NOT EXISTS (SELECT 1 FROM sys.columns
               WHERE object_id = OBJECT_ID(N'[dbo].[POS_InvoiceHeader_BC]')
                 AND name = N'BC_SalesOrderID')
BEGIN
    ALTER TABLE [dbo].[POS_InvoiceHeader_BC]
        ADD [BC_SalesOrderID] VARCHAR(255) NULL;
END
GO
