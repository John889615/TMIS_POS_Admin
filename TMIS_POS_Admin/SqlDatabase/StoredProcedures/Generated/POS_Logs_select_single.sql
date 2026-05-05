USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_Logs_select_single', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_Logs_select_single;
GO

CREATE PROCEDURE dbo.POS_Logs_select_single
    @AuditLogID INT
AS
BEGIN
    SELECT *
    FROM POS_Logs
    WHERE AuditLogID = @AuditLogID;
END
GO