USE [TMIS_Development]
GO

IF OBJECT_ID('dbo.POS_PurchaseOrders_insert', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_PurchaseOrders_insert;
GO

CREATE PROCEDURE dbo.POS_PurchaseOrders_insert
    @OrderNumber VARCHAR(50),
    @FK_SupplierID INT,
    @FK_DebtorID INT,
    @FK_CostCenterID INT = NULL,
    @FK_OrderStatusID INT,
    @FK_UserID INT,
    @Notes VARCHAR(MAX) = NULL,
    @ManagerNotes VARCHAR(MAX) = NULL,
    @DateOrdered DATETIME,
    @DateUpdated DATETIME = NULL
AS
BEGIN
    DECLARE @Inserted TABLE (PurchaseOrderID INT);

    INSERT INTO POS_PurchaseOrders (OrderNumber, FK_SupplierID, FK_DebtorID, FK_CostCenterID, FK_OrderStatusID, FK_UserID, Notes, ManagerNotes, DateOrdered, DateUpdated)
    OUTPUT INSERTED.PurchaseOrderID INTO @Inserted
    VALUES (@OrderNumber, @FK_SupplierID, @FK_DebtorID, @FK_CostCenterID, @FK_OrderStatusID, @FK_UserID, @Notes, @ManagerNotes, @DateOrdered, @DateUpdated);

    SELECT *
    FROM POS_PurchaseOrders
    WHERE PurchaseOrderID = 
    (
        SELECT TOP 1 PurchaseOrderID
        FROM @Inserted
    );
END
GO