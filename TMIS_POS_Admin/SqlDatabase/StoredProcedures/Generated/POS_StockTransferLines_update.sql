USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_StockTransferLines_update', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_StockTransferLines_update;
GO

CREATE PROCEDURE dbo.POS_StockTransferLines_update
    @StockTransferLineID INT,
    @FK_StockTransferID INT,
    @FK_ProductID INT,
    @Quantity DECIMAL (18, 4)
AS
BEGIN
    UPDATE POS_StockTransferLines
    SET     FK_StockTransferID = @FK_StockTransferID,
    FK_ProductID = @FK_ProductID,
    Quantity = @Quantity
    WHERE StockTransferLineID = @StockTransferLineID;

    SELECT *
    FROM POS_StockTransferLines
    WHERE StockTransferLineID = @StockTransferLineID;
END
GO