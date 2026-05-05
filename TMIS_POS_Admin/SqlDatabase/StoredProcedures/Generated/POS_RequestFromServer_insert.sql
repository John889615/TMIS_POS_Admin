USE [TMIS_Development]
GO

IF OBJECT_ID('dbo.POS_RequestFromServer_insert', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_RequestFromServer_insert;
GO

CREATE PROCEDURE dbo.POS_RequestFromServer_insert
    @Type VARCHAR(50),
    @LastRequestDate DATETIME = NULL,
    @CallSequence INT,
    @SyncFrequency INT,
    @IsActive BIT,
    @ApiUrl VARCHAR(255)
AS
BEGIN
    DECLARE @Inserted TABLE (RequestFromServerID INT);

    INSERT INTO POS_RequestFromServer ([Type], LastRequestDate, CallSequence, SyncFrequency, IsActive, ApiUrl)
    OUTPUT INSERTED.RequestFromServerID INTO @Inserted
    VALUES (@Type, @LastRequestDate, @CallSequence, @SyncFrequency, @IsActive, @ApiUrl);

    SELECT *
    FROM POS_RequestFromServer
    WHERE RequestFromServerID = 
    (
        SELECT TOP 1 RequestFromServerID
        FROM @Inserted
    );
END
GO