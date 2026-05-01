USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_PurchaseOrders_update', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_PurchaseOrders_update;
GO

CREATE PROCEDURE dbo.POS_PurchaseOrders_update
    @PurchaseOrderID INT,
    @OrderNumber VARCHAR(50),
    @FK_SupplierID INT,
    @FK_DebtorID INT,
    @FK_CostCenterID INT = NULL,
    @FK_OrderStatusID INT,
    @FK_UserID INT,
    @Notes VARCHAR(MAX) = NULL,
    @ManagerNotes VARCHAR(MAX) = NULL,
    @DateOrdered DATETIME,
    @DateUpdated DATETIME
AS
BEGIN
    UPDATE POS_PurchaseOrders
    SET     OrderNumber = @OrderNumber,
    FK_SupplierID = @FK_SupplierID,
    FK_DebtorID = @FK_DebtorID,
    FK_CostCenterID = @FK_CostCenterID,
    FK_OrderStatusID = @FK_OrderStatusID,
    FK_UserID = @FK_UserID,
    Notes = @Notes,
    ManagerNotes = @ManagerNotes,
    DateOrdered = @DateOrdered,
    DateUpdated = @DateUpdated
    WHERE PurchaseOrderID = @PurchaseOrderID;

    SELECT *
    FROM POS_PurchaseOrders
    WHERE PurchaseOrderID = @PurchaseOrderID;
END
GO