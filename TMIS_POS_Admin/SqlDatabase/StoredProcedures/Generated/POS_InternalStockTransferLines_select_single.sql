USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_InternalStockTransferLines_select_single', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_InternalStockTransferLines_select_single;
GO

CREATE PROCEDURE dbo.POS_InternalStockTransferLines_select_single
    @InternalStockTransferLineID INT
AS
BEGIN
    SELECT *
    FROM POS_InternalStockTransferLines
    WHERE InternalStockTransferLineID = @InternalStockTransferLineID;
END
GO