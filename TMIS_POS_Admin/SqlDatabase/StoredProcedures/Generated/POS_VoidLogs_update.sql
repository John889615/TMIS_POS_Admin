USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_VoidLogs_update', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_VoidLogs_update;
GO

CREATE PROCEDURE dbo.POS_VoidLogs_update
    @VoidLogID UNIQUEIDENTIFIER,
    @FK_TabID UNIQUEIDENTIFIER = NULL,
    @FK_TabLineID UNIQUEIDENTIFIER = NULL,
    @VoidedBy VARCHAR(255),
    @Note VARCHAR(MAX) = NULL,
    @DateCreated DATETIME,
    @DateUpdated DATETIME = NULL
AS
BEGIN
    UPDATE POS_VoidLogs
    SET     FK_TabID = @FK_TabID,
    FK_TabLineID = @FK_TabLineID,
    VoidedBy = @VoidedBy,
    Note = @Note,
    DateUpdated = @DateUpdated
    WHERE VoidLogID = @VoidLogID;

    SELECT *
    FROM POS_VoidLogs
    WHERE VoidLogID = @VoidLogID;
END
GO