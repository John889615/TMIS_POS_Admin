USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.DebtorTypeMappings_select_all', 'P') IS NOT NULL
    DROP PROCEDURE dbo.DebtorTypeMappings_select_all;
GO

CREATE PROCEDURE dbo.DebtorTypeMappings_select_all
AS
BEGIN
    SELECT *
    FROM DebtorTypeMappings;
END
GO