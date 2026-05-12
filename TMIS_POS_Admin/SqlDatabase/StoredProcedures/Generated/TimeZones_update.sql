USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.TimeZones_update', 'P') IS NOT NULL
    DROP PROCEDURE dbo.TimeZones_update;
GO

CREATE PROCEDURE dbo.TimeZones_update
    @TimeZoneID INT,
    @TimeZone VARCHAR(100),
    @UTCOffset VARCHAR(10),
    @ObservesDST BIT,
    @FK_CreatedUserID INT = NULL,
    @FK_UpdatedUserID INT = NULL,
    @DateCreated DATETIME,
    @DateUpdated DATETIME = NULL
AS
BEGIN
    UPDATE TimeZones
    SET     TimeZone = @TimeZone,
    UTCOffset = @UTCOffset,
    ObservesDST = @ObservesDST,
    FK_CreatedUserID = @FK_CreatedUserID,
    FK_UpdatedUserID = @FK_UpdatedUserID,
    DateUpdated = @DateUpdated
    WHERE TimeZoneID = @TimeZoneID;

    SELECT *
    FROM TimeZones
    WHERE TimeZoneID = @TimeZoneID;
END
GO