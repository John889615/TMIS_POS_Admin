USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_Logs_update', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_Logs_update;
GO

CREATE PROCEDURE dbo.POS_Logs_update
    @AuditLogID INT,
    @Action VARCHAR(255),
    @ItemID INT = NULL,
    @Item VARCHAR(255) = NULL,
    @FK_UserID INT,
    @ActionDate DATETIME
AS
BEGIN
    UPDATE POS_Logs
    SET     Action = @Action,
    ItemID = @ItemID,
    Item = @Item,
    FK_UserID = @FK_UserID,
    ActionDate = @ActionDate
    WHERE AuditLogID = @AuditLogID;

    SELECT *
    FROM POS_Logs
    WHERE AuditLogID = @AuditLogID;
END
GO