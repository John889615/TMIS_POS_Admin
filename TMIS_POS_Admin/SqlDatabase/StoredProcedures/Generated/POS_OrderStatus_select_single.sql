USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_OrderStatus_select_single', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_OrderStatus_select_single;
GO

CREATE PROCEDURE dbo.POS_OrderStatus_select_single
    @OrderStatusID INT
AS
BEGIN
    SELECT *
    FROM POS_OrderStatus
    WHERE OrderStatusID = @OrderStatusID;
END
GO