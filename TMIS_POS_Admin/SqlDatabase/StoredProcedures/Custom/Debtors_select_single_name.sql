USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.Debtors_select_single_name', 'P') IS NOT NULL
    DROP PROCEDURE dbo.Debtors_select_single_name;
GO

CREATE PROCEDURE dbo.Debtors_select_single_name
	@Name VARCHAR(255)
AS
BEGIN
    SELECT *
	FROM Debtors
	WHERE [Name] = @Name
END
GO