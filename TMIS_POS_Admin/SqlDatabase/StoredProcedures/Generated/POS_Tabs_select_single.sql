USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_Tabs_select_single', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_Tabs_select_single;
GO

CREATE PROCEDURE dbo.POS_Tabs_select_single
    @TabID UNIQUEIDENTIFIER
AS
BEGIN
    SELECT *
    FROM POS_Tabs
    WHERE TabID = @TabID;
END
GO