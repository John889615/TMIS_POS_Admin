USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.Entities_select_creditor', 'P') IS NOT NULL
    DROP PROCEDURE dbo.Entities_select_creditor;
GO

CREATE PROCEDURE dbo.Entities_select_creditor

AS
BEGIN
    SELECT *
FROM  Entities
WHERE [Name] = 'Creditors'
END
GO