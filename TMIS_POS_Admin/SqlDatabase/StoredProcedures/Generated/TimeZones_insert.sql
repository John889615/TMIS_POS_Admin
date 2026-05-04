USE [TMIS_Development]
GO

IF OBJECT_ID('dbo.TimeZones_insert', 'P') IS NOT NULL
    DROP PROCEDURE dbo.TimeZones_insert;
GO

CREATE PROCEDURE dbo.TimeZones_insert
    @TimeZone VARCHAR(100),
    @UTCOffset VARCHAR(10),
    @ObservesDST BIT
AS
BEGIN
    DECLARE @Inserted TABLE (TimeZoneID INT);

    INSERT INTO TimeZones (TimeZone, UTCOffset, ObservesDST)
    OUTPUT INSERTED.TimeZoneID INTO @Inserted
    VALUES (@TimeZone, @UTCOffset, @ObservesDST);

    SELECT *
    FROM TimeZones
    WHERE TimeZoneID = 
    (
        SELECT TOP 1 TimeZoneID
        FROM @Inserted
    );
END
GO