USE [TMIS_Development]
GO

IF OBJECT_ID('dbo.POS_InvoiceHeader_BC_select_candidates', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_InvoiceHeader_BC_select_candidates;
GO

-- =============================================================
-- POS_InvoiceHeader_BC_select_candidates
--   Returns paid, non-voided invoices that have not yet been
--   successfully pushed to BC. LEFT JOINs the extension table so
--   never-attempted invoices are returned (no extension row yet).
--
--   Ordering: oldest attempt first (NULL = never attempted, so
--   freshly arrived invoices land at the front).
--
--   Caller (BcPushHostedService) iterates and calls Bc_Push_Service
--   for each row.
-- =============================================================
CREATE PROCEDURE dbo.POS_InvoiceHeader_BC_select_candidates
    @MaxRows INT = 500
AS
BEGIN
    SET NOCOUNT ON;

    SELECT TOP (@MaxRows)
        ih.InvoiceHeaderID,
        ih.FK_LocationID,
        ih.InvoiceNo,
        ih.DateCreated,
        bc.BC_LastAttemptAt
      FROM [dbo].[POS_InvoiceHeaders] ih
      LEFT JOIN [dbo].[POS_InvoiceHeader_BC] bc
        ON bc.FK_InvoiceHeaderID = ih.InvoiceHeaderID
     WHERE ih.IsPaid   = 1
       AND ih.IsVoided = 0
       AND (bc.BC_InvoiceID IS NULL)
     ORDER BY
        CASE WHEN bc.BC_LastAttemptAt IS NULL THEN 0 ELSE 1 END,
        bc.BC_LastAttemptAt ASC,
        ih.DateCreated ASC;
END
GO
