USE [TMIS_Development]
GO

IF OBJECT_ID('dbo.POS_StockReceiveLines_insert', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_StockReceiveLines_insert;
GO

CREATE PROCEDURE dbo.POS_StockReceiveLines_insert
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
    DECLARE @Inserted TABLE (StockReceiveLineID INT);

    INSERT INTO POS_StockReceiveLines (FK_StockReceiveID, FK_ProductID, Quantity, UnitCostIncl, UnitCostExcl, FK_TaxTypeID, TaxRate, TotalCostIncl, TotalCostExcl, Notes, LineTotal)
    OUTPUT INSERTED.StockReceiveLineID INTO @Inserted
    VALUES (@FK_StockReceiveID, @FK_ProductID, @Quantity, @UnitCostIncl, @UnitCostExcl, @FK_TaxTypeID, @TaxRate, @TotalCostIncl, @TotalCostExcl, @Notes, @LineTotal);

    SELECT *
    FROM POS_StockReceiveLines
    WHERE StockReceiveLineID = 
    (
        SELECT TOP 1 StockReceiveLineID
        FROM @Inserted
    );
END
GO