USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_PurchaseOrderLines_select_single', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_PurchaseOrderLines_select_single;
GO

CREATE PROCEDURE dbo.POS_PurchaseOrderLines_select_single
    @PurchaseOrderLineID INT
AS
BEGIN
    SELECT *
    FROM POS_PurchaseOrderLines
    WHERE PurchaseOrderLineID = @PurchaseOrderLineID;
END
GO