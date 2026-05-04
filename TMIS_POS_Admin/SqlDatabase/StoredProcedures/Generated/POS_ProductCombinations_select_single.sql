USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_ProductCombinations_select_single', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_ProductCombinations_select_single;
GO

CREATE PROCEDURE dbo.POS_ProductCombinations_select_single
    @ProductCombinationID INT
AS
BEGIN
    SELECT *
    FROM POS_ProductCombinations
    WHERE ProductCombinationID = @ProductCombinationID;
END
GO