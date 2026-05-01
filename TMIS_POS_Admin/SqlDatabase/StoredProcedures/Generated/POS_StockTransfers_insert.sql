USE [TMIS_Development]
GO

IF OBJECT_ID('dbo.POS_StockTransfers_insert', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_StockTransfers_insert;
GO

CREATE PROCEDURE dbo.POS_StockTransfers_insert
    @RefNumber VARCHAR(50) = NULL,
    @FK_FromDebtorID INT,
    @FK_ToDebtorID INT,
    @FK_OrderStatusID INT,
    @FK_UserID INT,
    @DateTransfered DATETIME,
    @Notes VARCHAR(MAX) = NULL
AS
BEGIN
    DECLARE @Inserted TABLE (StockTransferID INT);

    INSERT INTO POS_StockTransfers (RefNumber, FK_FromDebtorID, FK_ToDebtorID, FK_OrderStatusID, FK_UserID, DateTransfered, Notes)
    OUTPUT INSERTED.StockTransferID INTO @Inserted
    VALUES (@RefNumber, @FK_FromDebtorID, @FK_ToDebtorID, @FK_OrderStatusID, @FK_UserID, @DateTransfered, @Notes);

    SELECT *
    FROM POS_StockTransfers
    WHERE StockTransferID = 
    (
        SELECT TOP 1 StockTransferID
        FROM @Inserted
    );
END
GO