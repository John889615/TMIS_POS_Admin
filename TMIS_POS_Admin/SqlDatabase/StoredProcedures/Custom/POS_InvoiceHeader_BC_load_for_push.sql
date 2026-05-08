USE [TMIS_Development]
GO

IF OBJECT_ID('dbo.POS_InvoiceHeader_BC_load_for_push', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_InvoiceHeader_BC_load_for_push;
GO

-- =============================================================
-- POS_InvoiceHeader_BC_load_for_push
--   Returns two result sets needed to build a BC sales order:
--     [0] Invoice header + Location.ShortCode + ExistingBcInvoiceID
--     [1] Invoice lines flattened with Product.BC_ID + per-line
--         LineLocationBcId resolved through the cost centre.
--
--   Caller (Bc_Push_Service) validates the header (IsPaid=1,
--   IsVoided=0, ExistingBcInvoiceID IS NULL) before calling BC.
--   If validation fails the call is short-circuited with an
--   appropriate result.
--
--   Per-line location resolution (2026-05-08):
--   To support multi-cost-centre tabs (e.g. drinks rung at the bar
--   and food rung at the kitchen on the same booking) each
--   salesOrderLine in BC needs its OWN locationId rather than the
--   header location. The chain is:
--     POS_InvoiceLines  -> POS_InvoiceTabs (FK_TabID)
--                       -> POS_TabLines    (matched on FK_TabID +
--                                           FK_ProductID; deterministic
--                                           tiebreaker on DateCreated)
--                       -> POS_CostCenters (FK_CostCenterID)
--                       -> POS_Locations   (FK_LocationID)
--                       -> BC_ID
--   When any link is missing (legacy lines without FK_CostCenterID,
--   cost centre with no BC_ID, etc.) we COALESCE down to the header
--   location's BC_ID so legacy invoices still push successfully.
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
        bc.BC_InvoiceNo    AS ExistingBcInvoiceNo,
        bc.BC_SalesOrderID AS ExistingBcSalesOrderID,
        bc.BC_SalesOrderNo AS ExistingBcSalesOrderNo
      FROM [dbo].[POS_InvoiceHeaders] ih
      INNER JOIN [dbo].[POS_Locations] l
        ON l.LocationID = ih.FK_LocationID
      LEFT JOIN [dbo].[POS_InvoiceHeader_BC] bc
        ON bc.FK_InvoiceHeaderID = ih.InvoiceHeaderID
     WHERE ih.InvoiceHeaderID = @InvoiceHeaderID;

    -- Lines (joined through InvoiceTabs to the Products table for BC_ID,
    -- and through TabLines/CostCenters/Locations for the per-line
    -- BC location). ProductName is the snapshot label that was on the
    -- POS line when it was created (POS_InvoiceLines.Product). It
    -- surfaces in error messages so the operator can identify which
    -- physical item is rejected by BC.
    --
    -- Header's location BC_ID is resolved once and used as the fallback
    -- in COALESCE for any line whose cost-centre chain breaks. The
    -- caller still validates the header location is non-null.
    DECLARE @HeaderLocationBcId VARCHAR(255);
    SELECT @HeaderLocationBcId = l.BC_ID
      FROM [dbo].[POS_InvoiceHeaders] ih
      INNER JOIN [dbo].[POS_Locations] l
        ON l.LocationID = ih.FK_LocationID
     WHERE ih.InvoiceHeaderID = @InvoiceHeaderID;

    SELECT
        il.InvoiceLineID,
        il.FK_InvoiceTabID,
        il.FK_ProductID,
        il.Product AS ProductName,
        p.BC_ID    AS ProductBcId,
        il.Quantity,
        il.LineDiscount,
        il.LineTotalExcl,
        il.LineTotalIncl,
        -- Cost-centre-derived location for this specific line, with a
        -- fallback to the header location when any link in the chain
        -- is missing. CROSS APPLY picks the earliest matching TabLine
        -- on (TabID, ProductID) so the result is deterministic when a
        -- product was rung up multiple times on the same tab.
        COALESCE(ccLoc.BC_ID, @HeaderLocationBcId) AS LineLocationBcId,
        cc.CostCenterID AS LineCostCenterID,
        ccLoc.LocationID AS LineLocationID
      FROM [dbo].[POS_InvoiceLines] il
      INNER JOIN [dbo].[POS_InvoiceTabs] it
        ON it.InvoiceTabID = il.FK_InvoiceTabID
      INNER JOIN [dbo].[POS_Products] p
        ON p.ProductID = il.FK_ProductID
      OUTER APPLY (
          SELECT TOP 1 tl.FK_CostCenterID
            FROM [dbo].[POS_TabLines] tl
           WHERE tl.FK_TabID     = it.FK_TabID
             AND tl.FK_ProductID = il.FK_ProductID
           ORDER BY tl.DateCreated ASC, tl.TabLineID ASC
      ) tlMatch
      LEFT JOIN [dbo].[POS_CostCenters] cc
        ON cc.CostCenterID = tlMatch.FK_CostCenterID
      LEFT JOIN [dbo].[POS_Locations] ccLoc
        ON ccLoc.LocationID = cc.FK_LocationID
     WHERE it.FK_InvoiceHeaderID = @InvoiceHeaderID
     ORDER BY il.InvoiceLineID;
END
GO
