USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_StockReceive_select_single', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_StockReceive_select_single;
GO

CREATE PROCEDURE dbo.POS_StockReceive_select_single
    @StockReceiveID INT
AS
BEGIN
    SELECT *
    FROM POS_StockReceive
    WHERE StockReceiveID = @StockReceiveID;
END
GO