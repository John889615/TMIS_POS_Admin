USE [TMIS_Development]
GO

IF OBJECT_ID('dbo.POS_VoidLogs_insert', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_VoidLogs_insert;
GO

CREATE PROCEDURE dbo.POS_VoidLogs_insert
    @VoidLogID UNIQUEIDENTIFIER = NULL,
    @FK_TabID UNIQUEIDENTIFIER = NULL,
    @FK_TabLineID UNIQUEIDENTIFIER = NULL,
    @VoidedBy VARCHAR(255),
    @Note VARCHAR(MAX) = NULL,
    @DateCreated DATETIME,
    @DateUpdated DATETIME
AS
BEGIN
    DECLARE @Inserted TABLE (VoidLogID UNIQUEIDENTIFIER);

    INSERT INTO POS_VoidLogs (VoidLogID, FK_TabID, FK_TabLineID, VoidedBy, Note, DateCreated, DateUpdated)
    OUTPUT INSERTED.VoidLogID INTO @Inserted
    VALUES (ISNULL(@VoidLogID, NEWID()), @FK_TabID, @FK_TabLineID, @VoidedBy, @Note, @DateCreated, @DateUpdated);

    SELECT *
    FROM POS_VoidLogs
    WHERE VoidLogID = 
    (
        SELECT TOP 1 VoidLogID
        FROM @Inserted
    );
END
GO