USE [TMIS_Development]
GO

IF OBJECT_ID('dbo.POS_StockTransferLines_insert', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_StockTransferLines_insert;
GO

CREATE PROCEDURE dbo.POS_StockTransferLines_insert
    @FK_StockTransferID INT,
    @FK_ProductID INT,
    @Quantity DECIMAL (18, 4)
AS
BEGIN
    DECLARE @Inserted TABLE (StockTransferLineID INT);

    INSERT INTO POS_StockTransferLines (FK_StockTransferID, FK_ProductID, Quantity)
    OUTPUT INSERTED.StockTransferLineID INTO @Inserted
    VALUES (@FK_StockTransferID, @FK_ProductID, @Quantity);

    SELECT *
    FROM POS_StockTransferLines
    WHERE StockTransferLineID = 
    (
        SELECT TOP 1 StockTransferLineID
        FROM @Inserted
    );
END
GO