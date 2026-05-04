USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.EntityAddresses_select_all', 'P') IS NOT NULL
    DROP PROCEDURE dbo.EntityAddresses_select_all;
GO

CREATE PROCEDURE dbo.EntityAddresses_select_all
AS
BEGIN
    SELECT *
    FROM EntityAddresses;
END
GO