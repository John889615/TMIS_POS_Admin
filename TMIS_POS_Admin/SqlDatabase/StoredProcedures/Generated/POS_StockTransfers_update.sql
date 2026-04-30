USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_StockTransfers_update', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_StockTransfers_update;
GO

CREATE PROCEDURE dbo.POS_StockTransfers_update
    @StockTransferID INT,
    @RefNumber VARCHAR(50) = NULL,
    @FK_FromDebtorID INT,
    @FK_ToDebtorID INT,
    @FK_OrderStatusID INT,
    @FK_UserID INT,
    @DateTransfered DATETIME,
    @Notes VARCHAR(MAX) = NULL
AS
BEGIN
    UPDATE POS_StockTransfers
    SET     RefNumber = @RefNumber,
    FK_FromDebtorID = @FK_FromDebtorID,
    FK_ToDebtorID = @FK_ToDebtorID,
    FK_OrderStatusID = @FK_OrderStatusID,
    FK_UserID = @FK_UserID,
    DateTransfered = @DateTransfered,
    Notes = @Notes
    WHERE StockTransferID = @StockTransferID;

    SELECT *
    FROM POS_StockTransfers
    WHERE StockTransferID = @StockTransferID;
END
GO