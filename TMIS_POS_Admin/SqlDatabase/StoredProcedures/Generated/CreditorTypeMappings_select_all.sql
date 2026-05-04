USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.CreditorTypeMappings_select_all', 'P') IS NOT NULL
    DROP PROCEDURE dbo.CreditorTypeMappings_select_all;
GO

CREATE PROCEDURE dbo.CreditorTypeMappings_select_all
AS
BEGIN
    SELECT *
    FROM CreditorTypeMappings;
END
GO