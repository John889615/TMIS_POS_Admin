USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.AddressTypes_select_all', 'P') IS NOT NULL
    DROP PROCEDURE dbo.AddressTypes_select_all;
GO

CREATE PROCEDURE dbo.AddressTypes_select_all
AS
BEGIN
    SELECT *
    FROM AddressTypes;
END
GO