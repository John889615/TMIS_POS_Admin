USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_ImageCategories_select_single', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_ImageCategories_select_single;
GO

CREATE PROCEDURE dbo.POS_ImageCategories_select_single
    @ImageCategoryID INT
AS
BEGIN
    SELECT *
    FROM POS_ImageCategories
    WHERE ImageCategoryID = @ImageCategoryID;
END
GO