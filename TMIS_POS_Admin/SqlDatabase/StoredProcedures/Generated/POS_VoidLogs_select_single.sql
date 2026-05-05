USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_VoidLogs_select_single', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_VoidLogs_select_single;
GO

CREATE PROCEDURE dbo.POS_VoidLogs_select_single
    @VoidLogID UNIQUEIDENTIFIER
AS
BEGIN
    SELECT *
    FROM POS_VoidLogs
    WHERE VoidLogID = @VoidLogID;
END
GO