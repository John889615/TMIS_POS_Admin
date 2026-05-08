USE [TMIS_Development]
GO

IF OBJECT_ID('dbo.POS_InvoiceHeader_BC_stamp_result', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_InvoiceHeader_BC_stamp_result;
GO

-- =============================================================
-- POS_InvoiceHeader_BC_stamp_result
--   Idempotent UPSERT into POS_InvoiceHeader_BC.
--
--   On success:
--     * Stamps BC_InvoiceID and BC_PushedAt.
--     * Clears BC_LastError.
--     * Stamps BC_LastAttemptAt.
--   On failure:
--     * Stamps BC_LastError (truncated to 4000 chars by caller).
--     * Stamps BC_LastAttemptAt.
--     * Leaves BC_InvoiceID alone.
--
--   Caller passes @Success = 1 with @BcInvoiceID populated, OR
--   @Success = 0 with @ErrorMessage populated.
-- =============================================================
CREATE PROCEDURE dbo.POS_InvoiceHeader_BC_stamp_result
    @InvoiceHeaderID UNIQUEIDENTIFIER,
    @Success         BIT,
    @BcInvoiceID     VARCHAR(255) = NULL,
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
               SET BC_InvoiceID     = CASE WHEN @Success = 1 THEN @BcInvoiceID ELSE BC_InvoiceID END,
                   BC_PushedAt      = CASE WHEN @Success = 1 THEN @Now         ELSE BC_PushedAt END,
                   BC_LastError     = CASE WHEN @Success = 1 THEN NULL         ELSE @ErrorMessage END,
                   BC_LastAttemptAt = @Now
             WHERE FK_InvoiceHeaderID = @InvoiceHeaderID;
        END
        ELSE
        BEGIN
            INSERT INTO [dbo].[POS_InvoiceHeader_BC]
                    (FK_InvoiceHeaderID, BC_InvoiceID, BC_PushedAt, BC_LastError, BC_LastAttemptAt)
            VALUES  (@InvoiceHeaderID,
                     CASE WHEN @Success = 1 THEN @BcInvoiceID ELSE NULL END,
                     CASE WHEN @Success = 1 THEN @Now         ELSE NULL END,
                     CASE WHEN @Success = 1 THEN NULL         ELSE @ErrorMessage END,
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
