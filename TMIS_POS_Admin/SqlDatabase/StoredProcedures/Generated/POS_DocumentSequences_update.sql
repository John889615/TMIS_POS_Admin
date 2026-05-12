USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_DocumentSequences_update', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_DocumentSequences_update;
GO

CREATE PROCEDURE dbo.POS_DocumentSequences_update
    @DocumentSequenceID INT,
    @DocumentType VARCHAR(50),
    @Prefix VARCHAR(10),
    @PadLength INT,
    @NextNumber BIGINT,
    @DateCreated DATETIME,
    @DateUpdated DATETIME
AS
BEGIN
    UPDATE POS_DocumentSequences
    SET     DocumentType = @DocumentType,
    Prefix = @Prefix,
    PadLength = @PadLength,
    NextNumber = @NextNumber,
    DateUpdated = @DateUpdated
    WHERE DocumentSequenceID = @DocumentSequenceID;

    SELECT *
    FROM POS_DocumentSequences
    WHERE DocumentSequenceID = @DocumentSequenceID;
END
GO