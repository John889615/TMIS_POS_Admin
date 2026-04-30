USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_StockTransfers_select_single', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_StockTransfers_select_single;
GO

CREATE PROCEDURE dbo.POS_StockTransfers_select_single
    @StockTransferID INT
AS
BEGIN
    SELECT *
    FROM POS_StockTransfers
    WHERE StockTransferID = @StockTransferID;
END
GO