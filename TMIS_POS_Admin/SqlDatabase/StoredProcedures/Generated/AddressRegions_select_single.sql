USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.AddressRegions_select_single', 'P') IS NOT NULL
    DROP PROCEDURE dbo.AddressRegions_select_single;
GO

CREATE PROCEDURE dbo.AddressRegions_select_single
    @AddressRegionID INT
AS
BEGIN
    SELECT *
    FROM AddressRegions
    WHERE AddressRegionID = @AddressRegionID;
END
GO