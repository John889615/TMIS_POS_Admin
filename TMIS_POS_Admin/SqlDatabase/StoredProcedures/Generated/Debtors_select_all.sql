USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.Debtors_select_all', 'P') IS NOT NULL
    DROP PROCEDURE dbo.Debtors_select_all;
GO

CREATE PROCEDURE dbo.Debtors_select_all
AS
BEGIN
    SELECT *
    FROM Debtors;
END
GO