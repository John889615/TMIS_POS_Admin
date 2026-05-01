USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_PurchaseOrders_select_single', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_PurchaseOrders_select_single;
GO

CREATE PROCEDURE dbo.POS_PurchaseOrders_select_single
    @PurchaseOrderID INT
AS
BEGIN
    SELECT *
    FROM POS_PurchaseOrders
    WHERE PurchaseOrderID = @PurchaseOrderID;
END
GO