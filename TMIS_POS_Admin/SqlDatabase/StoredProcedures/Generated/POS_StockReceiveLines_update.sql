USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_StockReceiveLines_update', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_StockReceiveLines_update;
GO

CREATE PROCEDURE dbo.POS_StockReceiveLines_update
    @StockReceiveLineID INT,
    @FK_StockReceiveID INT,
    @FK_ProductID INT,
    @Quantity DECIMAL (18, 4),
    @UnitCostIncl DECIMAL (18, 4) = NULL,
    @UnitCostExcl DECIMAL (18, 4) = NULL,
    @FK_TaxTypeID INT = NULL,
    @TaxRate DECIMAL (18, 4) = NULL,
    @TotalCostIncl DECIMAL (18, 4) = NULL,
    @TotalCostExcl DECIMAL (18, 4) = NULL,
    @Notes VARCHAR(MAX) = NULL,
    @LineTotal DECIMAL (18, 4) = NULL
AS
BEGIN
    UPDATE POS_StockReceiveLines
    SET     FK_StockReceiveID = @FK_StockReceiveID,
    FK_ProductID = @FK_ProductID,
    Quantity = @Quantity,
    UnitCostIncl = @UnitCostIncl,
    UnitCostExcl = @UnitCostExcl,
    FK_TaxTypeID = @FK_TaxTypeID,
    TaxRate = @TaxRate,
    TotalCostIncl = @TotalCostIncl,
    TotalCostExcl = @TotalCostExcl,
    Notes = @Notes,
    LineTotal = @LineTotal
    WHERE StockReceiveLineID = @StockReceiveLineID;

    SELECT *
    FROM POS_StockReceiveLines
    WHERE StockReceiveLineID = @StockReceiveLineID;
END
GO