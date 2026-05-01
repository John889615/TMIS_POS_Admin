USE [TMIS_Development]
GO

IF OBJECT_ID('dbo.POS_OrderStatus_insert', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_OrderStatus_insert;
GO

CREATE PROCEDURE dbo.POS_OrderStatus_insert
    @OrderStatus VARCHAR(50)
AS
BEGIN
    DECLARE @Inserted TABLE (OrderStatusID INT);

    INSERT INTO POS_OrderStatus (OrderStatus)
    OUTPUT INSERTED.OrderStatusID INTO @Inserted
    VALUES (@OrderStatus);

    SELECT *
    FROM POS_OrderStatus
    WHERE OrderStatusID = 
    (
        SELECT TOP 1 OrderStatusID
        FROM @Inserted
    );
END
GO