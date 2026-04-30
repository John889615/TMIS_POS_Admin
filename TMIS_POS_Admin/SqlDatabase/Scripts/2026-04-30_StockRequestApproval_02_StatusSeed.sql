-- =============================================================
-- Migration : Stock Request Approval workflow - status seed
-- Date      : 2026-04-30
-- Step      : 02 of N
--
-- Adds two missing OrderStatus rows the workflow needs:
--   * Draft             - request being built, not yet submitted
--   * PartiallyApproved - some lines approved, some declined / partial qty
--
-- Existing relevant rows used as-is (per Tables/POS_OrderStatus.sql):
--   1 = Pending           -> "submitted, awaiting approval"
--   2 = Approved          -> "fully approved"
--   4 = Cancelled         -> "declined"
--   5 = Draft              (added by this script)
--   6 = PartiallyApproved  (added by this script)
--
-- (Note: id 3 = Received is left for the PO domain; not used by stock requests.)
--
-- Idempotent - safe to re-run.
-- =============================================================
USE [TMIS_Development];
GO

IF NOT EXISTS (SELECT 1 FROM [dbo].[POS_OrderStatus] WHERE OrderStatus = N'Draft')
    INSERT INTO [dbo].[POS_OrderStatus] (OrderStatus) VALUES (N'Draft');
GO

IF NOT EXISTS (SELECT 1 FROM [dbo].[POS_OrderStatus] WHERE OrderStatus = N'PartiallyApproved')
    INSERT INTO [dbo].[POS_OrderStatus] (OrderStatus) VALUES (N'PartiallyApproved');
GO

-- Verify result. Expected rows: Pending, Approved, Received, Cancelled, Draft, PartiallyApproved.
SELECT OrderStatusID, OrderStatus FROM [dbo].[POS_OrderStatus] ORDER BY OrderStatusID;
