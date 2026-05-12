USE [TMIS_Development]
GO

IF OBJECT_ID('dbo.POS_DocumentSequences_insert', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_DocumentSequences_insert;
GO

CREATE PROCEDURE dbo.POS_DocumentSequences_insert
    @DocumentType VARCHAR(50),
    @Prefix VARCHAR(10),
    @PadLength INT,
    @NextNumber BIGINT,
    @DateCreated DATETIME,
    @DateUpdated DATETIME
AS
BEGIN
    DECLARE @Inserted TABLE (DocumentSequenceID INT);

    INSERT INTO POS_DocumentSequences (DocumentType, Prefix, PadLength, NextNumber, DateCreated, DateUpdated)
    OUTPUT INSERTED.DocumentSequenceID INTO @Inserted
    VALUES (@DocumentType, @Prefix, @PadLength, @NextNumber, @DateCreated, @DateUpdated);

    SELECT *
    FROM POS_DocumentSequences
    WHERE DocumentSequenceID = 
    (
        SELECT TOP 1 DocumentSequenceID
        FROM @Inserted
    );
END
GO