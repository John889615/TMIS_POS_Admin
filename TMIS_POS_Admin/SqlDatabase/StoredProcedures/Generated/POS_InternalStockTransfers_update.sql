USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_InternalStockTransfers_update', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_InternalStockTransfers_update;
GO

CREATE PROCEDURE dbo.POS_InternalStockTransfers_update
    @InternalStockTransferID INT,
    @RefNumber VARCHAR(50) = NULL,
    @FK_DebtorID INT,
    @FK_CostCenterID INT,
    @FK_UserID INT,
    @Notes VARCHAR(MAX) = NULL,
    @DateTransfered DATETIME
AS
BEGIN
    UPDATE POS_InternalStockTransfers
    SET     RefNumber = @RefNumber,
    FK_DebtorID = @FK_DebtorID,
    FK_CostCenterID = @FK_CostCenterID,
    FK_UserID = @FK_UserID,
    Notes = @Notes,
    DateTransfered = @DateTransfered
    WHERE InternalStockTransferID = @InternalStockTransferID;

    SELECT *
    FROM POS_InternalStockTransfers
    WHERE InternalStockTransferID = @InternalStockTransferID;
END
GO