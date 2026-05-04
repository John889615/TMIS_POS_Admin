USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_ProductPreparation_select_single', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_ProductPreparation_select_single;
GO

CREATE PROCEDURE dbo.POS_ProductPreparation_select_single
    @ProductPreparationID INT
AS
BEGIN
    SELECT *
    FROM POS_ProductPreparation
    WHERE ProductPreparationID = @ProductPreparationID;
END
GO