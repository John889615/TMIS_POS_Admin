USE [TMIS_Development]
GO

IF OBJECT_ID('dbo.POS_InvoiceHeader_BC_select_voided', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_InvoiceHeader_BC_select_voided;
GO

-- =============================================================
-- POS_InvoiceHeader_BC_select_voided
--   Returns voided invoices for the Admin UI grid. The C# layer
--   splits the rows into VoidedAndPushed / VoidedAndNotPushed by
--   inspecting BC_InvoiceID.
-- =============================================================
CREATE PROCEDURE dbo.POS_InvoiceHeader_BC_select_voided
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        ih.InvoiceHeaderID,
        ih.InvoiceNo,
        ih.PartyName,
        ih.BookingReference,
        ih.InclTotal,
        ih.VoidedDate,
        ih.VoidedBy,
        ih.VoidReason,
        bc.BC_InvoiceID,
        bc.BC_InvoiceNo,
        bc.BC_SalesOrderID,
        bc.BC_SalesOrderNo,
        bc.BC_PushedAt
      FROM [dbo].[POS_InvoiceHeaders] ih
      LEFT JOIN [dbo].[POS_InvoiceHeader_BC] bc
        ON bc.FK_InvoiceHeaderID = ih.InvoiceHeaderID
     WHERE ih.IsVoided = 1
     ORDER BY ih.VoidedDate DESC;
END
GO
