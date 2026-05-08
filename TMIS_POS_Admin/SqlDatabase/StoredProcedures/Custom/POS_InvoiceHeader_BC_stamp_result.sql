USE [TMIS_Development]
GO

IF OBJECT_ID('dbo.POS_InvoiceHeader_BC_stamp_result', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_InvoiceHeader_BC_stamp_result;
GO

-- =============================================================
-- POS_InvoiceHeader_BC_stamp_result
--   Idempotent UPSERT into POS_InvoiceHeader_BC, plus a row in
--   POS_InvoiceHeader_BC_AuditLog so the per-attempt history is
--   preserved (BC_LastError on the extension table only holds the
--   most-recent error - this audit log keeps them all).
--
--   Caller passes whichever fields are known at this stage. NULL
--   or empty parameters preserve the existing extension row value
--   via COALESCE(NULLIF(...,''),...). On the audit log every call
--   is a fresh row regardless of NULLs.
--
--   Stage values used by Bc_Push_Service:
--     CreateOrder    - POST salesOrders
--     AddLine        - POST salesOrderLines (one per line)
--     ShipAndInvoice - bound action
--     Validate       - pre-flight checks
--     Resume         - reusing existing BC sales order
--     OrderOnly      - AutoPost=false placeholder commit
--   Outcome values:  Success | Failure
-- =============================================================
CREATE PROCEDURE dbo.POS_InvoiceHeader_BC_stamp_result
    @InvoiceHeaderID UNIQUEIDENTIFIER,
    @Success         BIT,
    @BcInvoiceID     VARCHAR(255) = NULL,
    @BcInvoiceNo     VARCHAR(50)  = NULL,
    @BcSalesOrderID  VARCHAR(255) = NULL,
    @BcSalesOrderNo  VARCHAR(50)  = NULL,
    @Stage           VARCHAR(50)  = NULL,
    @ErrorMessage    VARCHAR(MAX) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    IF @InvoiceHeaderID IS NULL
        THROW 50001, 'POS_InvoiceHeader_BC_stamp_result: @InvoiceHeaderID is required.', 1;

    DECLARE @Now DATETIME = GETDATE();
    DECLARE @Outcome VARCHAR(20) = CASE WHEN @Success = 1 THEN 'Success' ELSE 'Failure' END;

    BEGIN TRY
        BEGIN TRAN;

        -- 1. Extension table: latest known state (UPSERT)
        IF EXISTS (SELECT 1 FROM [dbo].[POS_InvoiceHeader_BC]
                   WHERE FK_InvoiceHeaderID = @InvoiceHeaderID)
        BEGIN
            UPDATE [dbo].[POS_InvoiceHeader_BC]
               SET BC_InvoiceID     = COALESCE(NULLIF(@BcInvoiceID,    ''), BC_InvoiceID),
                   BC_InvoiceNo     = COALESCE(NULLIF(@BcInvoiceNo,    ''), BC_InvoiceNo),
                   BC_SalesOrderID  = COALESCE(NULLIF(@BcSalesOrderID, ''), BC_SalesOrderID),
                   BC_SalesOrderNo  = COALESCE(NULLIF(@BcSalesOrderNo, ''), BC_SalesOrderNo),
                   BC_PushedAt      = CASE WHEN @Success = 1 AND NULLIF(@BcInvoiceID, '') IS NOT NULL THEN @Now ELSE BC_PushedAt END,
                   BC_LastError     = CASE WHEN @Success = 1 THEN NULL ELSE @ErrorMessage END,
                   BC_LastAttemptAt = @Now
             WHERE FK_InvoiceHeaderID = @InvoiceHeaderID;
        END
        ELSE
        BEGIN
            INSERT INTO [dbo].[POS_InvoiceHeader_BC]
                    (InvoiceHeaderBcID, FK_InvoiceHeaderID,
                     BC_InvoiceID, BC_InvoiceNo,
                     BC_SalesOrderID, BC_SalesOrderNo,
                     BC_PushedAt, BC_LastError, BC_LastAttemptAt)
            VALUES  (NEWID(), @InvoiceHeaderID,
                     NULLIF(@BcInvoiceID,    ''),
                     NULLIF(@BcInvoiceNo,    ''),
                     NULLIF(@BcSalesOrderID, ''),
                     NULLIF(@BcSalesOrderNo, ''),
                     CASE WHEN @Success = 1 AND NULLIF(@BcInvoiceID, '') IS NOT NULL THEN @Now ELSE NULL END,
                     CASE WHEN @Success = 1 THEN NULL ELSE @ErrorMessage END,
                     @Now);
        END

        -- 2. Audit log: append-only history of every attempt
        INSERT INTO [dbo].[POS_InvoiceHeader_BC_AuditLog]
                (InvoiceHeaderBcAuditLogID, FK_InvoiceHeaderID, AttemptedAt,
                 Stage, Outcome,
                 BC_SalesOrderID, BC_SalesOrderNo,
                 BC_InvoiceID,    BC_InvoiceNo,
                 ErrorMessage)
        VALUES  (NEWID(), @InvoiceHeaderID, @Now,
                 @Stage, @Outcome,
                 NULLIF(@BcSalesOrderID, ''), NULLIF(@BcSalesOrderNo, ''),
                 NULLIF(@BcInvoiceID,    ''), NULLIF(@BcInvoiceNo,    ''),
                 @ErrorMessage);

        COMMIT TRAN;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRAN;
        THROW;
    END CATCH
END
GO
