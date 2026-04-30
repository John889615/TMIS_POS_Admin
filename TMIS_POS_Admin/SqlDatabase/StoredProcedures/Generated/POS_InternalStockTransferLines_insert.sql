USE [TMIS_Development]
GO

IF OBJECT_ID('dbo.POS_InternalStockTransferLines_insert', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_InternalStockTransferLines_insert;
GO

CREATE PROCEDURE dbo.POS_InternalStockTransferLines_insert
    @FK_InternalStockTransferID INT,
    @FK_ProductID INT,
    @Quantity DECIMAL (18, 4)
AS
BEGIN
    DECLARE @Inserted TABLE (InternalStockTransferLineID INT);

    INSERT INTO POS_InternalStockTransferLines (FK_InternalStockTransferID, FK_ProductID, Quantity)
    OUTPUT INSERTED.InternalStockTransferLineID INTO @Inserted
    VALUES (@FK_InternalStockTransferID, @FK_ProductID, @Quantity);

    SELECT *
    FROM POS_InternalStockTransferLines
    WHERE InternalStockTransferLineID = 
    (
        SELECT TOP 1 InternalStockTransferLineID
        FROM @Inserted
    );
END
GO