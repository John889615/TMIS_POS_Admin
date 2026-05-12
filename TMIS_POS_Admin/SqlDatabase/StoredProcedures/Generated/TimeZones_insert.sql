USE [TMIS_Development]
GO

IF OBJECT_ID('dbo.TimeZones_insert', 'P') IS NOT NULL
    DROP PROCEDURE dbo.TimeZones_insert;
GO

CREATE PROCEDURE dbo.TimeZones_insert
    @TimeZone VARCHAR(100),
    @UTCOffset VARCHAR(10),
    @ObservesDST BIT,
    @FK_CreatedUserID INT = NULL,
    @FK_UpdatedUserID INT = NULL,
    @DateCreated DATETIME,
    @DateUpdated DATETIME = NULL
AS
BEGIN
    DECLARE @Inserted TABLE (TimeZoneID INT);

    INSERT INTO TimeZones (TimeZone, UTCOffset, ObservesDST, FK_CreatedUserID, FK_UpdatedUserID, DateCreated, DateUpdated)
    OUTPUT INSERTED.TimeZoneID INTO @Inserted
    VALUES (@TimeZone, @UTCOffset, @ObservesDST, @FK_CreatedUserID, @FK_UpdatedUserID, @DateCreated, @DateUpdated);

    SELECT *
    FROM TimeZones
    WHERE TimeZoneID = 
    (
        SELECT TOP 1 TimeZoneID
        FROM @Inserted
    );
END
GO