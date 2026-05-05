USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.Entities_select_debtor', 'P') IS NOT NULL
    DROP PROCEDURE dbo.Entities_select_debtor;
GO

CREATE PROCEDURE dbo.Entities_select_debtor

AS
BEGIN
    SELECT *
FROM  Entities
WHERE [Name] = 'Locations'
END
GO