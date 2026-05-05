USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_ProductPreparationMethods_select_single', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_ProductPreparationMethods_select_single;
GO

CREATE PROCEDURE dbo.POS_ProductPreparationMethods_select_single
    @ProductPreparationMethodID INT
AS
BEGIN
    SELECT *
    FROM POS_ProductPreparationMethods
    WHERE ProductPreparationMethodID = @ProductPreparationMethodID;
END
GO