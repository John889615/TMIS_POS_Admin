USE [TMIS_Development]
GO

IF OBJECT_ID('dbo.POS_InternalStockTransfers_insert', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_InternalStockTransfers_insert;
GO

CREATE PROCEDURE dbo.POS_InternalStockTransfers_insert
    @RefNumber VARCHAR(50) = NULL,
    @FK_DebtorID INT,
    @FK_CostCenterID INT,
    @FK_UserID INT,
    @Notes VARCHAR(MAX) = NULL,
    @DateTransfered DATETIME
AS
BEGIN
    DECLARE @Inserted TABLE (InternalStockTransferID INT);

    INSERT INTO POS_InternalStockTransfers (RefNumber, FK_DebtorID, FK_CostCenterID, FK_UserID, Notes, DateTransfered)
    OUTPUT INSERTED.InternalStockTransferID INTO @Inserted
    VALUES (@RefNumber, @FK_DebtorID, @FK_CostCenterID, @FK_UserID, @Notes, @DateTransfered);

    SELECT *
    FROM POS_InternalStockTransfers
    WHERE InternalStockTransferID = 
    (
        SELECT TOP 1 InternalStockTransferID
        FROM @Inserted
    );
END
GO