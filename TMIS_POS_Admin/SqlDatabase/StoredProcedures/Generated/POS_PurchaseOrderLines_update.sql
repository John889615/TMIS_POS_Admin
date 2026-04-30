USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_PurchaseOrderLines_update', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_PurchaseOrderLines_update;
GO

CREATE PROCEDURE dbo.POS_PurchaseOrderLines_update
    @PurchaseOrderLineID INT,
    @FK_PurchaseOrderID INT,
    @FK_ProductID INT,
    @Quantity DECIMAL (18, 4),
    @UnitCostIncl DECIMAL (18, 4),
    @UnitCostExcl DECIMAL (18, 4),
    @FK_TaxTypeID INT,
    @TaxRate DECIMAL (18, 4),
    @TotalCostIncl DECIMAL (18, 4),
    @TotalCostExcl DECIMAL (18, 4),
    @Notes VARCHAR(MAX) = NULL,
    @ManagerNotes VARCHAR(MAX) = NULL,
    @IsDeclined BIT
AS
BEGIN
    UPDATE POS_PurchaseOrderLines
    SET     FK_PurchaseOrderID = @FK_PurchaseOrderID,
    FK_ProductID = @FK_ProductID,
    Quantity = @Quantity,
    UnitCostIncl = @UnitCostIncl,
    UnitCostExcl = @UnitCostExcl,
    FK_TaxTypeID = @FK_TaxTypeID,
    TaxRate = @TaxRate,
    TotalCostIncl = @TotalCostIncl,
    TotalCostExcl = @TotalCostExcl,
    Notes = @Notes,
    ManagerNotes = @ManagerNotes,
    IsDeclined = @IsDeclined
    WHERE PurchaseOrderLineID = @PurchaseOrderLineID;

    SELECT *
    FROM POS_PurchaseOrderLines
    WHERE PurchaseOrderLineID = @PurchaseOrderLineID;
END
GO