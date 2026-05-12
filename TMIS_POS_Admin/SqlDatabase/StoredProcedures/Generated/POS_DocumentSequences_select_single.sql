USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_DocumentSequences_select_single', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_DocumentSequences_select_single;
GO

CREATE PROCEDURE dbo.POS_DocumentSequences_select_single
    @DocumentSequenceID INT
AS
BEGIN
    SELECT *
    FROM POS_DocumentSequences
    WHERE DocumentSequenceID = @DocumentSequenceID;
END
GO