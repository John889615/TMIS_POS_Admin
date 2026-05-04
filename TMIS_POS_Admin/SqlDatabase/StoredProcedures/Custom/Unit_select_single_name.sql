USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.Unit_select_single_name', 'P') IS NOT NULL
    DROP PROCEDURE dbo.Unit_select_single_name;
GO

CREATE PROCEDURE dbo.Unit_select_single_name
	@Unit VARCHAR(255)
AS
BEGIN
    SELECT *
	FROM POS_Units
	WHERE Unit = @Unit
END
GO