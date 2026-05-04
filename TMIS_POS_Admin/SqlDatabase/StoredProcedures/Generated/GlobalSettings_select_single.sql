USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.GlobalSettings_select_single', 'P') IS NOT NULL
    DROP PROCEDURE dbo.GlobalSettings_select_single;
GO

CREATE PROCEDURE dbo.GlobalSettings_select_single
    @GlobalSettingID INT
AS
BEGIN
    SELECT *
    FROM GlobalSettings
    WHERE GlobalSettingID = @GlobalSettingID;
END
GO