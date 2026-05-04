USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_RequestFromServer_select_single', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_RequestFromServer_select_single;
GO

CREATE PROCEDURE dbo.POS_RequestFromServer_select_single
    @RequestFromServerID INT
AS
BEGIN
    SELECT *
    FROM POS_RequestFromServer
    WHERE RequestFromServerID = @RequestFromServerID;
END
GO