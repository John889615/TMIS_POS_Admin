USE [TMIS_BlueSafaris]
GO

IF OBJECT_ID('dbo.BulkUpsertToServer_TabLineGuests', 'P') IS NOT NULL
    DROP PROCEDURE dbo.BulkUpsertToServer_TabLineGuests;
GO

CREATE PROCEDURE dbo.BulkUpsertToServer_TabLineGuests
    @Rows dbo.BulkInsertToServer READONLY
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    BEGIN TRY
        BEGIN TRAN;

        ;WITH Src AS
        (
            SELECT
                Guid1 AS TabLineGuestID,
                Guid2 AS FK_TabLineID,
                Int1 AS FK_GuestID,
                CAST(String1 AS VARCHAR(MAX)) AS Note,
                Date1 AS DateUpdated,
                CAST(String2 AS VARCHAR(50)) AS SyncStatus
            FROM @Rows
            WHERE Guid1 IS NOT NULL
        )
        SELECT * INTO #Src FROM Src;

        DELETE tlg
        FROM dbo.POS_TabLineGuests tlg
        INNER JOIN #Src s
            ON s.TabLineGuestID = tlg.TabLineGuestID
        WHERE s.SyncStatus = 'DELETE_PENDING';

        ;WITH UpsertSrc AS
        (
            SELECT TabLineGuestID, FK_TabLineID, FK_GuestID, Note, DateUpdated
            FROM #Src
            WHERE ISNULL(SyncStatus, 'NOT_SYNCED') <> 'DELETE_PENDING'
        )
        MERGE dbo.POS_TabLineGuests AS T
        USING UpsertSrc AS S
          ON T.TabLineGuestID = S.TabLineGuestID
        WHEN MATCHED THEN
            UPDATE SET
                T.FK_TabLineID = S.FK_TabLineID,
                T.FK_GuestID = S.FK_GuestID,
                T.Note = S.Note,
                T.DateUpdated = S.DateUpdated
        WHEN NOT MATCHED BY TARGET THEN
            INSERT (TabLineGuestID, FK_TabLineID, FK_GuestID, Note, DateUpdated)
            VALUES (S.TabLineGuestID, S.FK_TabLineID, S.FK_GuestID, S.Note, S.DateUpdated);

        COMMIT TRAN;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRAN;
        THROW;
    END CATCH
END
GO