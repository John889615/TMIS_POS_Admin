USE [TMIS_Development]
GO

IF OBJECT_ID('dbo.POS_InvoiceHeader_BC_load_for_push', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_InvoiceHeader_BC_load_for_push;
GO

-- =============================================================
-- POS_InvoiceHeader_BC_load_for_push
--   Returns two result sets needed to build a BC sales order:
--     [0] Invoice header + Location.ShortCode + ExistingBcInvoiceID
--     [1] Invoice lines flattened with Product.BC_ID
--
--   Caller (Bc_Push_Service) validates the header (IsPaid=1,
--   IsVoided=0, ExistingBcInvoiceID IS NULL) before calling BC.
--   If validation fails the call is short-circuited with an
--   appropriate result.
-- =============================================================
CREATE PROCEDURE dbo.POS_InvoiceHeader_BC_load_for_push
    @InvoiceHeaderID UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    -- Header
    -- LocationBcId carries POS_Locations.BC_ID (the BC Location GUID).
    -- BC's salesOrderLine resource expects "locationId" = GUID; the
    -- string "locationCode" property does NOT exist on that type.
    -- LocationCode (ShortCode) is kept for diagnostics / future use.
    SELECT
        ih.InvoiceHeaderID,
        ih.InvoiceNo,
        ih.DateCreated,
        ih.IsPaid,
        ih.IsVoided,
        ih.FK_LocationID,
        l.ShortCode AS LocationCode,
        l.BC_ID     AS LocationBcId,
        bc.BC_InvoiceID    AS ExistingBcInvoiceID,
        bc.BC_SalesOrderID AS ExistingBcSalesOrderID
      FROM [dbo].[POS_InvoiceHeaders] ih
      INNER JOIN [dbo].[POS_Locations] l
        ON l.LocationID = ih.FK_LocationID
      LEFT JOIN [dbo].[POS_InvoiceHeader_BC] bc
        ON bc.FK_InvoiceHeaderID = ih.InvoiceHeaderID
     WHERE ih.InvoiceHeaderID = @InvoiceHeaderID;

    -- Lines (joined through InvoiceTabs to the Products table for BC_ID).
    -- ProductName is the snapshot label that was on the POS line when it was
    -- created (POS_InvoiceLines.Product). It surfaces in error messages so
    -- the operator can identify which physical item is rejected by BC.
    SELECT
        il.InvoiceLineID,
        il.FK_InvoiceTabID,
        il.FK_ProductID,
        il.Product AS ProductName,
        p.BC_ID    AS ProductBcId,
        il.Quantity,
        il.LineDiscount,
        il.LineTotalExcl,
        il.LineTotalIncl
      FROM [dbo].[POS_InvoiceLines] il
      INNER JOIN [dbo].[POS_InvoiceTabs] it
        ON it.InvoiceTabID = il.FK_InvoiceTabID
      INNER JOIN [dbo].[POS_Products] p
        ON p.ProductID = il.FK_ProductID
     WHERE it.FK_InvoiceHeaderID = @InvoiceHeaderID
     ORDER BY il.InvoiceLineID;
END
GO
