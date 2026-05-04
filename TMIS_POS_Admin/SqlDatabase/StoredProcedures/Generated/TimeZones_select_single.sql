USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.TimeZones_select_single', 'P') IS NOT NULL
    DROP PROCEDURE dbo.TimeZones_select_single;
GO

CREATE PROCEDURE dbo.TimeZones_select_single
    @TimeZoneID INT
AS
BEGIN
    SELECT *
    FROM TimeZones
    WHERE TimeZoneID = @TimeZoneID;
END
GO