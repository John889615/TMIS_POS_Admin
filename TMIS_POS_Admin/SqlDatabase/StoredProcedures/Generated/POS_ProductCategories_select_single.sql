USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_ProductCategories_select_single', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_ProductCategories_select_single;
GO

CREATE PROCEDURE dbo.POS_ProductCategories_select_single
    @ProductCategoryID INT
AS
BEGIN
    SELECT *
    FROM POS_ProductCategories
    WHERE ProductCategoryID = @ProductCategoryID;
END
GO