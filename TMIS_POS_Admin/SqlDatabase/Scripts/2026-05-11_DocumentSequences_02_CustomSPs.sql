-- =============================================================
-- Migration : Document Sequences - Next Reference SP
-- Date      : 2026-05-11
-- Step      : 02 of 02 (Custom SP. Run AFTER Step 01.)
--
-- Atomically mints the next reference number for a document type:
--   exec POS_DocumentSequences_Next @DocumentType = 'StockRequest';
--   -> returns one row { RefNumber = 'SR00001' }
--
-- Uses (UPDLOCK, HOLDLOCK, ROWLOCK) so concurrent callers serialize
-- on the row for that document type and never see the same number.
-- Idempotent (CREATE OR ALTER).
-- =============================================================
USE [TMIS_Development];
GO

CREATE OR ALTER PROCEDURE [dbo].[POS_DocumentSequences_Next]
    @DocumentType VARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    DECLARE @Prefix     VARCHAR(10);
    DECLARE @PadLength  INT;
    DECLARE @Number     BIGINT;

    BEGIN TRANSACTION;

        SELECT
            @Prefix    = Prefix,
            @PadLength = PadLength,
            @Number    = NextNumber
        FROM [dbo].[POS_DocumentSequences] WITH (UPDLOCK, HOLDLOCK, ROWLOCK)
        WHERE DocumentType = @DocumentType;

        IF @Number IS NULL
        BEGIN
            ROLLBACK TRANSACTION;
            ;THROW 50001, 'Document type is not configured in POS_DocumentSequences.', 1;
            RETURN;
        END;

        UPDATE [dbo].[POS_DocumentSequences]
        SET NextNumber  = NextNumber + 1,
            DateUpdated = GETDATE()
        WHERE DocumentType = @DocumentType;

    COMMIT TRANSACTION;

    SELECT
        @Prefix + RIGHT(REPLICATE('0', @PadLength) + CAST(@Number AS VARCHAR(20)), @PadLength) AS RefNumber;
END
GO
