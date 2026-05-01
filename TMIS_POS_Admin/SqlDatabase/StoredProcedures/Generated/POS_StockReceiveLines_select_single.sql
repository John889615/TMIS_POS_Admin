USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_StockReceiveLines_select_single', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_StockReceiveLines_select_single;
GO

CREATE PROCEDURE dbo.POS_StockReceiveLines_select_single
    @StockReceiveLineID INT
AS
BEGIN
    SELECT *
    FROM POS_StockReceiveLines
    WHERE StockReceiveLineID = @StockReceiveLineID;
END
GO