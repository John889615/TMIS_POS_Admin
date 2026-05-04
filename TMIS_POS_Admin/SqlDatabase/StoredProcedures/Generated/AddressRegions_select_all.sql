USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.AddressRegions_select_all', 'P') IS NOT NULL
    DROP PROCEDURE dbo.AddressRegions_select_all;
GO

CREATE PROCEDURE dbo.AddressRegions_select_all
AS
BEGIN
    SELECT *
    FROM AddressRegions;
END
GO