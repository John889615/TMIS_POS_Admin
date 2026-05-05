USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.EntitySettings_select_single', 'P') IS NOT NULL
    DROP PROCEDURE dbo.EntitySettings_select_single;
GO

CREATE PROCEDURE dbo.EntitySettings_select_single
    @EntitySettingID INT
AS
BEGIN
    SELECT *
    FROM EntitySettings
    WHERE EntitySettingID = @EntitySettingID;
END
GO