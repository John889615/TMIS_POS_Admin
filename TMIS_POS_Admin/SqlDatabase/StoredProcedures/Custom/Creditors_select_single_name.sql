USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.Creditors_select_single_name', 'P') IS NOT NULL
    DROP PROCEDURE dbo.Creditors_select_single_name;
GO

CREATE PROCEDURE dbo.Creditors_select_single_name
	@Name VARCHAR(255)
AS
BEGIN
    SELECT *
	FROM Creditors
	WHERE [Name] = @Name
END
GO