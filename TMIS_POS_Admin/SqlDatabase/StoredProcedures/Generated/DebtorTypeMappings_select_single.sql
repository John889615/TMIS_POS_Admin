USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.DebtorTypeMappings_select_single', 'P') IS NOT NULL
    DROP PROCEDURE dbo.DebtorTypeMappings_select_single;
GO

CREATE PROCEDURE dbo.DebtorTypeMappings_select_single

AS
BEGIN
    SELECT *
    FROM DebtorTypeMappings
    WHERE ;
END
GO