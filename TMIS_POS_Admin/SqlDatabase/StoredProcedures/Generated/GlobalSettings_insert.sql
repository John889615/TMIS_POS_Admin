USE [TMIS_Development]
GO

IF OBJECT_ID('dbo.GlobalSettings_insert', 'P') IS NOT NULL
    DROP PROCEDURE dbo.GlobalSettings_insert;
GO

CREATE PROCEDURE dbo.GlobalSettings_insert
    @Key VARCHAR(255),
    @Value VARCHAR(255),
    @Environment VARCHAR(40),
    @FK_CreatedUserID INT,
    @FK_UpdatedUserID INT,
    @DateCreated DATETIME,
    @DateUpdated DATETIME = NULL
AS
BEGIN
    DECLARE @Inserted TABLE (GlobalSettingID INT);

    INSERT INTO GlobalSettings ([Key], [Value], Environment, FK_CreatedUserID, FK_UpdatedUserID, DateCreated, DateUpdated)
    OUTPUT INSERTED.GlobalSettingID INTO @Inserted
    VALUES (@Key, @Value, @Environment, @FK_CreatedUserID, @FK_UpdatedUserID, @DateCreated, @DateUpdated);

    SELECT *
    FROM GlobalSettings
    WHERE GlobalSettingID = 
    (
        SELECT TOP 1 GlobalSettingID
        FROM @Inserted
    );
END
GO