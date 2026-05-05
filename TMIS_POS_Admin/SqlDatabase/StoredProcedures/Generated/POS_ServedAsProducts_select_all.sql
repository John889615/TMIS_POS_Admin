USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_ServedAsProducts_select_all', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_ServedAsProducts_select_all;
GO

CREATE PROCEDURE dbo.POS_ServedAsProducts_select_all
AS
BEGIN
    SELECT *
    FROM POS_ServedAsProducts;
END
GO