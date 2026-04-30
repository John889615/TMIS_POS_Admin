USE [TMIS_Development]
GO

IF OBJECT_ID('dbo.POS_PurchaseOrderLines_insert', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_PurchaseOrderLines_insert;
GO

CREATE PROCEDURE dbo.POS_PurchaseOrderLines_insert
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
    DECLARE @Inserted TABLE (PurchaseOrderLineID INT);

    INSERT INTO POS_PurchaseOrderLines (FK_PurchaseOrderID, FK_ProductID, Quantity, UnitCostIncl, UnitCostExcl, FK_TaxTypeID, TaxRate, TotalCostIncl, TotalCostExcl, Notes, ManagerNotes, IsDeclined)
    OUTPUT INSERTED.PurchaseOrderLineID INTO @Inserted
    VALUES (@FK_PurchaseOrderID, @FK_ProductID, @Quantity, @UnitCostIncl, @UnitCostExcl, @FK_TaxTypeID, @TaxRate, @TotalCostIncl, @TotalCostExcl, @Notes, @ManagerNotes, @IsDeclined);

    SELECT *
    FROM POS_PurchaseOrderLines
    WHERE PurchaseOrderLineID = 
    (
        SELECT TOP 1 PurchaseOrderLineID
        FROM @Inserted
    );
END
GO