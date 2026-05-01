USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_StockRequests_select_single', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_StockRequests_select_single;
GO

CREATE PROCEDURE dbo.POS_StockRequests_select_single
    @StockRequestID INT
AS
BEGIN
    SELECT *
    FROM POS_StockRequests
    WHERE StockRequestID = @StockRequestID;
END
GO