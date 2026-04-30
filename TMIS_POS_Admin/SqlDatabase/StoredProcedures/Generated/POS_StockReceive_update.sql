USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_StockReceive_update', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_StockReceive_update;
GO

CREATE PROCEDURE dbo.POS_StockReceive_update
    @StockReceiveID INT,
    @FK_PurchaseOrderID INT = NULL,
    @FK_StockTransferID INT = NULL,
    @FK_UserID INT,
    @Notes VARCHAR(MAX) = NULL,
    @DateReceived DATETIME
AS
BEGIN
    UPDATE POS_StockReceive
    SET     FK_PurchaseOrderID = @FK_PurchaseOrderID,
    FK_StockTransferID = @FK_StockTransferID,
    FK_UserID = @FK_UserID,
    Notes = @Notes,
    DateReceived = @DateReceived
    WHERE StockReceiveID = @StockReceiveID;

    SELECT *
    FROM POS_StockReceive
    WHERE StockReceiveID = @StockReceiveID;
END
GO