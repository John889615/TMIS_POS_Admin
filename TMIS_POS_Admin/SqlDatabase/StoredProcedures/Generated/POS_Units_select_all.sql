USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_Units_select_all', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_Units_select_all;
GO

CREATE PROCEDURE dbo.POS_Units_select_all
AS
BEGIN
    SELECT *
    FROM POS_Units;
END
GO