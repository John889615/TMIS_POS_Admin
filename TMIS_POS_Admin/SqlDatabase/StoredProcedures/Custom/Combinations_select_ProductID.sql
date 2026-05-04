USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.Combinations_select_ProductID', 'P') IS NOT NULL
    DROP PROCEDURE dbo.Combinations_select_ProductID;
GO

CREATE PROCEDURE dbo.Combinations_select_ProductID
	@FK_ProductID INT
AS
BEGIN
    SELECT *
	FROM POS_ProductCombinations
	WHERE FK_ProductID = @FK_ProductID
END
GO