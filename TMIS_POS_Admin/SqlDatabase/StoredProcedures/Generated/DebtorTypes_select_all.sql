USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.DebtorTypes_select_all', 'P') IS NOT NULL
    DROP PROCEDURE dbo.DebtorTypes_select_all;
GO

CREATE PROCEDURE dbo.DebtorTypes_select_all
AS
BEGIN
    SELECT *
    FROM DebtorTypes;
END
GO