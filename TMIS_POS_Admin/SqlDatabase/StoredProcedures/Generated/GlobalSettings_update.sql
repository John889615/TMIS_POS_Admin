USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.GlobalSettings_update', 'P') IS NOT NULL
    DROP PROCEDURE dbo.GlobalSettings_update;
GO

CREATE PROCEDURE dbo.GlobalSettings_update
    @GlobalSettingID INT,
    @Key VARCHAR(255),
    @Value VARCHAR(255),
    @Environment VARCHAR(40),
    @FK_CreatedUserID INT,
    @FK_UpdatedUserID INT = NULL,
    @DateCreated DATETIME,
    @DateUpdated DATETIME = NULL
AS
BEGIN
    UPDATE GlobalSettings
    SET     [Key] = @Key,
    [Value] = @Value,
    Environment = @Environment,
    FK_CreatedUserID = @FK_CreatedUserID,
    FK_UpdatedUserID = @FK_UpdatedUserID,
    DateUpdated = @DateUpdated
    WHERE GlobalSettingID = @GlobalSettingID;

    SELECT *
    FROM GlobalSettings
    WHERE GlobalSettingID = @GlobalSettingID;
END
GO