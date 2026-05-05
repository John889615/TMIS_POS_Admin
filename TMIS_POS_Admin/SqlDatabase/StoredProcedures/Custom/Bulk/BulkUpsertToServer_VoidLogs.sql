USE [TMIS_BlueSafaris]
GO

IF OBJECT_ID('dbo.BulkUpsertToServer_VoidLogs', 'P') IS NOT NULL
    DROP PROCEDURE dbo.BulkUpsertToServer_VoidLogs;
GO

CREATE PROCEDURE dbo.BulkUpsertToServer_VoidLogs
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
                Guid1 AS VoidLogID,
                Guid2 AS FK_TabID,
                Guid3 AS FK_TabLineID,
                String2 AS VoidedBy,
                CAST(String1 AS VARCHAR(MAX)) AS Note,
                Date1 AS DateCreated,
                Date2 AS DateUpdated,
                CAST(String3 AS VARCHAR(50)) AS SyncStatus
            FROM @Rows
            WHERE Guid1 IS NOT NULL
        )
        SELECT * INTO #Src FROM Src;

        DELETE vl
        FROM dbo.POS_VoidLogs vl
        INNER JOIN #Src s
            ON s.VoidLogID = vl.VoidLogID
        WHERE s.SyncStatus = 'DELETE_PENDING';

        ;WITH UpsertSrc AS
        (
            SELECT VoidLogID, FK_TabID, FK_TabLineID, VoidedBy, Note, DateCreated, DateUpdated
            FROM #Src
            WHERE ISNULL(SyncStatus, 'NOT_SYNCED') <> 'DELETE_PENDING'
        )
        MERGE dbo.POS_VoidLogs AS T
        USING UpsertSrc AS S
          ON T.VoidLogID = S.VoidLogID
        WHEN MATCHED THEN
            UPDATE SET
                T.FK_TabID = S.FK_TabID,
                T.FK_TabLineID = S.FK_TabLineID,
                T.VoidedBy = S.VoidedBy,
                T.Note = S.Note,
                T.DateCreated = S.DateCreated,
                T.DateUpdated = S.DateUpdated
        WHEN NOT MATCHED BY TARGET THEN
            INSERT (VoidLogID, FK_TabID, FK_TabLineID, VoidedBy, Note, DateCreated, DateUpdated)
            VALUES (S.VoidLogID, S.FK_TabID, S.FK_TabLineID, S.VoidedBy, S.Note, S.DateCreated, S.DateUpdated);

        COMMIT TRAN;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRAN;
        THROW;
    END CATCH
END
GO