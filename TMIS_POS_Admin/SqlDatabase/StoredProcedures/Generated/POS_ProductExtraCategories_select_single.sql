USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_ProductExtraCategories_select_single', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_ProductExtraCategories_select_single;
GO

CREATE PROCEDURE dbo.POS_ProductExtraCategories_select_single
    @ProductExtraCategoryID INT
AS
BEGIN
    SELECT *
    FROM POS_ProductExtraCategories
    WHERE ProductExtraCategoryID = @ProductExtraCategoryID;
END
GO