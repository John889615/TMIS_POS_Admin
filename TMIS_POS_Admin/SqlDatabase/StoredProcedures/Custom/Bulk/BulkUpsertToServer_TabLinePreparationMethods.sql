USE [TMIS_BlueSafaris]
GO

IF OBJECT_ID('dbo.BulkUpsertToServer_TabLinePreparationMethods', 'P') IS NOT NULL
    DROP PROCEDURE dbo.BulkUpsertToServer_TabLinePreparationMethods;
GO

CREATE PROCEDURE dbo.BulkUpsertToServer_TabLinePreparationMethods
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
                Guid1 AS TabLinePreparationMethodID,
                Guid2 AS FK_TabLineCombinationID,
                Int1 AS FK_PreparationMethodID,
                CAST(String1 AS VARCHAR(255)) AS PreparationMethodName,
                CAST(String2 AS VARCHAR(50)) AS SyncStatus
            FROM @Rows
            WHERE Guid1 IS NOT NULL
        )
        SELECT * INTO #Src FROM Src;

        DELETE tpm
        FROM dbo.POS_TabLinePreparationMethods tpm
        INNER JOIN #Src s
            ON s.TabLinePreparationMethodID = tpm.TabLinePreparationMethodID
        WHERE s.SyncStatus = 'DELETE_PENDING';

        ;WITH UpsertSrc AS
        (
            SELECT TabLinePreparationMethodID, FK_TabLineCombinationID, FK_PreparationMethodID, PreparationMethodName
            FROM #Src
            WHERE ISNULL(SyncStatus, 'NOT_SYNCED') <> 'DELETE_PENDING'
        )
        MERGE dbo.POS_TabLinePreparationMethods AS T
        USING UpsertSrc AS S
          ON T.TabLinePreparationMethodID = S.TabLinePreparationMethodID
        WHEN MATCHED THEN
            UPDATE SET
                T.FK_TabLineCombinationID = S.FK_TabLineCombinationID,
                T.FK_PreparationMethodID = S.FK_PreparationMethodID,
                T.PreparationMethodName = S.PreparationMethodName
        WHEN NOT MATCHED BY TARGET THEN
            INSERT (TabLinePreparationMethodID, FK_TabLineCombinationID, FK_PreparationMethodID, PreparationMethodName)
            VALUES (S.TabLinePreparationMethodID, S.FK_TabLineCombinationID, S.FK_PreparationMethodID, S.PreparationMethodName);

        COMMIT TRAN;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRAN;
        THROW;
    END CATCH
END
GO