USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_ProductTypes_select_single', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_ProductTypes_select_single;
GO

CREATE PROCEDURE dbo.POS_ProductTypes_select_single
    @ProductTypeID INT
AS
BEGIN
    SELECT *
    FROM POS_ProductTypes
    WHERE ProductTypeID = @ProductTypeID;
END
GO