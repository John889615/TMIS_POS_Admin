USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.Extras_select_ProductID', 'P') IS NOT NULL
    DROP PROCEDURE dbo.Extras_select_ProductID;
GO

CREATE PROCEDURE dbo.Extras_select_ProductID
	@FK_ProductID INT
AS
BEGIN
    SELECT *
	FROM POS_ProductExtras
	WHERE FK_ProductID = @FK_ProductID
END
GO