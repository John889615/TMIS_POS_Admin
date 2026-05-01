USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_StockTransferLines_select_single', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_StockTransferLines_select_single;
GO

CREATE PROCEDURE dbo.POS_StockTransferLines_select_single
    @StockTransferLineID INT
AS
BEGIN
    SELECT *
    FROM POS_StockTransferLines
    WHERE StockTransferLineID = @StockTransferLineID;
END
GO