USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.Category_select_single_name', 'P') IS NOT NULL
    DROP PROCEDURE dbo.Category_select_single_name;
GO

CREATE PROCEDURE dbo.Category_select_single_name
	@CategoryName VARCHAR(255)
AS
BEGIN
    SELECT *
	FROM POS_ProductCategories
	WHERE CategoryName = @CategoryName
END
GO