USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_InternalStockTransferLines_update', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_InternalStockTransferLines_update;
GO

CREATE PROCEDURE dbo.POS_InternalStockTransferLines_update
    @InternalStockTransferLineID INT,
    @FK_InternalStockTransferID INT,
    @FK_ProductID INT,
    @Quantity DECIMAL (18, 4)
AS
BEGIN
    UPDATE POS_InternalStockTransferLines
    SET     FK_InternalStockTransferID = @FK_InternalStockTransferID,
    FK_ProductID = @FK_ProductID,
    Quantity = @Quantity
    WHERE InternalStockTransferLineID = @InternalStockTransferLineID;

    SELECT *
    FROM POS_InternalStockTransferLines
    WHERE InternalStockTransferLineID = @InternalStockTransferLineID;
END
GO