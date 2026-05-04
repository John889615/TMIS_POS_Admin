USE [TMIS_Development]
GO

IF OBJECT_ID('dbo.POS_VoidLogs_insert', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_VoidLogs_insert;
GO

CREATE PROCEDURE dbo.POS_VoidLogs_insert
    @FK_TabID UNIQUEIDENTIFIER = NULL,
    @FK_TabLineID UNIQUEIDENTIFIER = NULL,
    @VoidedBy VARCHAR(255),
    @Note VARCHAR(MAX) = NULL,
    @DateCreated DATETIME,
    @DateUpdated DATETIME
AS
BEGIN
    DECLARE @Inserted TABLE (VoidLogID UNIQUEIDENTIFIER);

    INSERT INTO POS_VoidLogs (FK_TabID, FK_TabLineID, VoidedBy, Note, DateCreated, DateUpdated)
    OUTPUT INSERTED.VoidLogID INTO @Inserted
    VALUES (@FK_TabID, @FK_TabLineID, @VoidedBy, @Note, @DateCreated, @DateUpdated);

    SELECT *
    FROM POS_VoidLogs
    WHERE VoidLogID = 
    (
        SELECT TOP 1 VoidLogID
        FROM @Inserted
    );
END
GO