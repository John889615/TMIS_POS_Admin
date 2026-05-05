USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.TimeZones_update', 'P') IS NOT NULL
    DROP PROCEDURE dbo.TimeZones_update;
GO

CREATE PROCEDURE dbo.TimeZones_update
    @TimeZoneID INT,
    @TimeZone VARCHAR(100),
    @UTCOffset VARCHAR(10),
    @ObservesDST BIT
AS
BEGIN
    UPDATE TimeZones
    SET     TimeZone = @TimeZone,
    UTCOffset = @UTCOffset,
    ObservesDST = @ObservesDST
    WHERE TimeZoneID = @TimeZoneID;

    SELECT *
    FROM TimeZones
    WHERE TimeZoneID = @TimeZoneID;
END
GO