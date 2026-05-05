USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.CreditorTypes_select_all', 'P') IS NOT NULL
    DROP PROCEDURE dbo.CreditorTypes_select_all;
GO

CREATE PROCEDURE dbo.CreditorTypes_select_all
AS
BEGIN
    SELECT *
    FROM CreditorTypes;
END
GO