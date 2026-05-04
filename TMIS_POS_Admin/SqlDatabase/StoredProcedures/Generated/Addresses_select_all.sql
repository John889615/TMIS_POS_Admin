USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.Addresses_select_all', 'P') IS NOT NULL
    DROP PROCEDURE dbo.Addresses_select_all;
GO

CREATE PROCEDURE dbo.Addresses_select_all
AS
BEGIN
    SELECT *
    FROM Addresses;
END
GO