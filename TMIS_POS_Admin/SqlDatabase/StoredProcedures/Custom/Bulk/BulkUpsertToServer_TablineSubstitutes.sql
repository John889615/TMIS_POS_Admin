USE [TMIS_BlueSafaris]
GO

IF OBJECT_ID('dbo.BulkUpsertToServer_TablineSubstitutes', 'P') IS NOT NULL
    DROP PROCEDURE dbo.BulkUpsertToServer_TablineSubstitutes;
GO

CREATE PROCEDURE dbo.BulkUpsertToServer_TablineSubstitutes
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
                Guid1 AS TablineSubstituteID,
                Guid2 AS FK_ParentTabLineID,
                Guid3 AS FK_SubstituionTabLineID,
                Guid4 AS FK_ParentTabLineCombinationID,
                CAST(String1 AS VARCHAR(50)) AS SyncStatus
            FROM @Rows
            WHERE Guid1 IS NOT NULL
        )
        SELECT * INTO #Src FROM Src;

        DELETE ts
        FROM dbo.POS_TablineSubstitutes ts
        INNER JOIN #Src s
            ON s.TablineSubstituteID = ts.POS_TablineSubstituteID
        WHERE s.SyncStatus = 'DELETE_PENDING';

        ;WITH UpsertSrc AS
        (
            SELECT TablineSubstituteID, FK_ParentTabLineID, FK_SubstituionTabLineID, FK_ParentTabLineCombinationID
            FROM #Src
            WHERE ISNULL(SyncStatus, 'NOT_SYNCED') <> 'DELETE_PENDING'
        )
        MERGE dbo.POS_TablineSubstitutes AS T
        USING UpsertSrc AS S
          ON T.POS_TablineSubstituteID = S.TablineSubstituteID
        WHEN MATCHED THEN
            UPDATE SET
                T.FK_ParentTabLineID = S.FK_ParentTabLineID,
                T.FK_SubstituionTabLineID = S.FK_SubstituionTabLineID,
                T.FK_ParentTabLineCombinationID = S.FK_ParentTabLineCombinationID
        WHEN NOT MATCHED BY TARGET THEN
            INSERT (POS_TablineSubstituteID, FK_ParentTabLineID, FK_SubstituionTabLineID, FK_ParentTabLineCombinationID)
            VALUES (S.TablineSubstituteID, S.FK_ParentTabLineID, S.FK_SubstituionTabLineID, S.FK_ParentTabLineCombinationID);

        COMMIT TRAN;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRAN;
        THROW;
    END CATCH
END
GO