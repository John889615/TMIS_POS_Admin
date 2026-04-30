USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_InternalStockTransfers_select_single', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_InternalStockTransfers_select_single;
GO

CREATE PROCEDURE dbo.POS_InternalStockTransfers_select_single
    @InternalStockTransferID INT
AS
BEGIN
    SELECT *
    FROM POS_InternalStockTransfers
    WHERE InternalStockTransferID = @InternalStockTransferID;
END
GO