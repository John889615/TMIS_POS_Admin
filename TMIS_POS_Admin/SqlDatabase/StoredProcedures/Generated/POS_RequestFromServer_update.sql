USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_RequestFromServer_update', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_RequestFromServer_update;
GO

CREATE PROCEDURE dbo.POS_RequestFromServer_update
    @RequestFromServerID INT,
    @Type VARCHAR(50),
    @LastRequestDate DATETIME = NULL,
    @CallSequence INT,
    @SyncFrequency INT,
    @IsActive BIT,
    @ApiUrl VARCHAR(255)
AS
BEGIN
    UPDATE POS_RequestFromServer
    SET     [Type] = @Type,
    LastRequestDate = @LastRequestDate,
    CallSequence = @CallSequence,
    SyncFrequency = @SyncFrequency,
    IsActive = @IsActive,
    ApiUrl = @ApiUrl
    WHERE RequestFromServerID = @RequestFromServerID;

    SELECT *
    FROM POS_RequestFromServer
    WHERE RequestFromServerID = @RequestFromServerID;
END
GO