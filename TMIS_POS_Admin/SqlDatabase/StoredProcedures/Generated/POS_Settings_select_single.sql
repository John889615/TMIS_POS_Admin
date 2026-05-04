USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_Settings_select_single', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_Settings_select_single;
GO

CREATE PROCEDURE dbo.POS_Settings_select_single
    @SettingID INT
AS
BEGIN
    SELECT *
    FROM POS_Settings
    WHERE SettingID = @SettingID;
END
GO