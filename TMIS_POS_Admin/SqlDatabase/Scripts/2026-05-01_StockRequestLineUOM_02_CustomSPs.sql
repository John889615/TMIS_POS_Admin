-- =============================================================
-- Migration : Stock Request Line - Unit of Measure
-- Date      : 2026-05-01
-- Step      : 02 of 02 (Custom SPs. Run AFTER the code generator.)
--
-- Replaces the custom select SP so the line list returns the unit name
-- and symbol alongside the product. Idempotent (CREATE OR ALTER).
-- =============================================================
USE [TMIS_Development];
GO

-- =============================================================
-- List lines for a given stock request (with product name, unit, approval state)
-- =============================================================
CREATE OR ALTER PROCEDURE [dbo].[stockRequestLines_select_all_stockRequestLines]
    @FK_StockRequestID INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        srl.StockRequestLineID,
        srl.FK_StockRequestID,
        srl.FK_ProductID,
        p.ProductName,
        srl.FK_UnitID,
        u.Unit,
        u.Symbol,
        srl.Quantity,
        srl.Notes,
        srl.ManagerNotes,
        srl.IsDeclined,
        srl.ApprovedQuantity
    FROM         dbo.POS_StockRequestLines srl
    INNER JOIN   dbo.POS_Products          p   ON p.ProductID = srl.FK_ProductID
    LEFT  JOIN   dbo.POS_Units             u   ON u.UnitID    = srl.FK_UnitID
    WHERE srl.FK_StockRequestID = @FK_StockRequestID
    ORDER BY srl.StockRequestLineID;
END
GO
