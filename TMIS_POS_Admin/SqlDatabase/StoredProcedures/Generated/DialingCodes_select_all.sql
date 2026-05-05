USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.DialingCodes_select_all', 'P') IS NOT NULL
    DROP PROCEDURE dbo.DialingCodes_select_all;
GO

CREATE PROCEDURE dbo.DialingCodes_select_all
AS
BEGIN
    SELECT *
    FROM DialingCodes;
END
GO