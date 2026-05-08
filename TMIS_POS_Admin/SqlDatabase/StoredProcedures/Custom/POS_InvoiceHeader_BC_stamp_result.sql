USE [TMIS_Development]
GO

IF OBJECT_ID('dbo.POS_InvoiceHeader_BC_stamp_result', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_InvoiceHeader_BC_stamp_result;
GO

-- =============================================================
-- POS_InvoiceHeader_BC_stamp_result
--   Idempotent UPSERT into POS_InvoiceHeader_BC.
--
--   Three orthogonal stamps the caller can drive (any combination):
--     * BC_SalesOrderID  - stamped right after BC creates the order
--                          (before lines / before shipAndInvoice).
--                          Survives failures so the next attempt can
--                          reuse the existing BC order rather than
--                          create a zombie duplicate.
--     * BC_InvoiceID     - stamped after a successful shipAndInvoice
--                          (or the "ORDER:<guid>" placeholder when
--                          BusinessCentral.AutoPost = false).
--     * BC_LastError     - stamped on any failed attempt.
--
--   Pass non-null parameters for the fields you want to update.
--   NULL or '' parameters preserve the existing value (no overwrite).
--
--   On every call BC_LastAttemptAt is bumped to NOW.
--   On Success=1, BC_LastError is cleared.
--   When @BcInvoiceID is provided AND Success=1, BC_PushedAt is set.
-- =============================================================
CREATE PROCEDURE dbo.POS_InvoiceHeader_BC_stamp_result
    @InvoiceHeaderID UNIQUEIDENTIFIER,
    @Success         BIT,
    @BcInvoiceID     VARCHAR(255) = NULL,
    @BcSalesOrderID  VARCHAR(255) = NULL,
    @ErrorMessage    VARCHAR(MAX) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    IF @InvoiceHeaderID IS NULL
        THROW 50001, 'POS_InvoiceHeader_BC_stamp_result: @InvoiceHeaderID is required.', 1;

    DECLARE @Now DATETIME = GETDATE();

    BEGIN TRY
        BEGIN TRAN;

        IF EXISTS (SELECT 1 FROM [dbo].[POS_InvoiceHeader_BC]
                   WHERE FK_InvoiceHeaderID = @InvoiceHeaderID)
        BEGIN
            UPDATE [dbo].[POS_InvoiceHeader_BC]
               SET BC_InvoiceID     = COALESCE(NULLIF(@BcInvoiceID,    ''), BC_InvoiceID),
                   BC_SalesOrderID  = COALESCE(NULLIF(@BcSalesOrderID, ''), BC_SalesOrderID),
                   BC_PushedAt      = CASE WHEN @Success = 1 AND NULLIF(@BcInvoiceID, '') IS NOT NULL THEN @Now ELSE BC_PushedAt END,
                   BC_LastError     = CASE WHEN @Success = 1 THEN NULL ELSE @ErrorMessage END,
                   BC_LastAttemptAt = @Now
             WHERE FK_InvoiceHeaderID = @InvoiceHeaderID;
        END
        ELSE
        BEGIN
            INSERT INTO [dbo].[POS_InvoiceHeader_BC]
                    (FK_InvoiceHeaderID, BC_InvoiceID, BC_SalesOrderID, BC_PushedAt, BC_LastError, BC_LastAttemptAt)
            VALUES  (@InvoiceHeaderID,
                     NULLIF(@BcInvoiceID,    ''),
                     NULLIF(@BcSalesOrderID, ''),
                     CASE WHEN @Success = 1 AND NULLIF(@BcInvoiceID, '') IS NOT NULL THEN @Now ELSE NULL END,
                     CASE WHEN @Success = 1 THEN NULL ELSE @ErrorMessage END,
                     @Now);
        END

        COMMIT TRAN;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRAN;
        THROW;
    END CATCH
END
GO
