USE [TMIS_Development]
GO

IF OBJECT_ID('dbo.POS_Logs_insert', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_Logs_insert;
GO

CREATE PROCEDURE dbo.POS_Logs_insert
    @Action VARCHAR(255),
    @ItemID INT = NULL,
    @Item VARCHAR(255) = NULL,
    @FK_UserID INT,
    @ActionDate DATETIME
AS
BEGIN
    DECLARE @Inserted TABLE (AuditLogID INT);

    INSERT INTO POS_Logs (Action, ItemID, Item, FK_UserID, ActionDate)
    OUTPUT INSERTED.AuditLogID INTO @Inserted
    VALUES (@Action, @ItemID, @Item, @FK_UserID, @ActionDate);

    SELECT *
    FROM POS_Logs
    WHERE AuditLogID = 
    (
        SELECT TOP 1 AuditLogID
        FROM @Inserted
    );
END
GO