USE [TMIS_Development]
GO

IF OBJECT_ID('dbo.POS_StockReceive_insert', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_StockReceive_insert;
GO

CREATE PROCEDURE dbo.POS_StockReceive_insert
    @FK_PurchaseOrderID INT = NULL,
    @FK_StockTransferID INT = NULL,
    @FK_UserID INT,
    @Notes VARCHAR(MAX) = NULL,
    @DateReceived DATETIME
AS
BEGIN
    DECLARE @Inserted TABLE (StockReceiveID INT);

    INSERT INTO POS_StockReceive (FK_PurchaseOrderID, FK_StockTransferID, FK_UserID, Notes, DateReceived)
    OUTPUT INSERTED.StockReceiveID INTO @Inserted
    VALUES (@FK_PurchaseOrderID, @FK_StockTransferID, @FK_UserID, @Notes, @DateReceived);

    SELECT *
    FROM POS_StockReceive
    WHERE StockReceiveID = 
    (
        SELECT TOP 1 StockReceiveID
        FROM @Inserted
    );
END
GO