USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_StockRequestLines_select_single', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_StockRequestLines_select_single;
GO

CREATE PROCEDURE dbo.POS_StockRequestLines_select_single
    @StockRequestLineID INT
AS
BEGIN
    SELECT *
    FROM POS_StockRequestLines
    WHERE StockRequestLineID = @StockRequestLineID;
END
GO