USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_Products_select_single', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_Products_select_single;
GO

CREATE PROCEDURE dbo.POS_Products_select_single
    @ProductID INT
AS
BEGIN
    SELECT *
    FROM POS_Products
    WHERE ProductID = @ProductID;
END
GO