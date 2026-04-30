USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_OrderStatus_update', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_OrderStatus_update;
GO

CREATE PROCEDURE dbo.POS_OrderStatus_update
    @OrderStatusID INT,
    @OrderStatus VARCHAR(50)
AS
BEGIN
    UPDATE POS_OrderStatus
    SET     OrderStatus = @OrderStatus
    WHERE OrderStatusID = @OrderStatusID;

    SELECT *
    FROM POS_OrderStatus
    WHERE OrderStatusID = @OrderStatusID;
END
GO