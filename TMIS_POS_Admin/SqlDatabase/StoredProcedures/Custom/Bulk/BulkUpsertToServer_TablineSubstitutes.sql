USE [TMIS_Development]
GO

IF OBJECT_ID('dbo.BulkUpsertToServer_TablineSubstitutes', 'P') IS NOT NULL
    DROP PROCEDURE dbo.BulkUpsertToServer_TablineSubstitutes;
GO

-- =============================================================
-- Updated 2026-05-06 for FOH schema reconciliation (Spec 1):
--   - PK column renamed POS_TablineSubstituteID -> TablineSubstituteID.
--     All references in MERGE/INSERT updated.
--
-- Slot mapping unchanged from prior version.
-- =============================================================
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
            ON s.TablineSubstituteID = ts.TablineSubstituteID
        WHERE s.SyncStatus = 'DELETE_PENDING';

        ;WITH UpsertSrc AS
        (
            SELECT TablineSubstituteID, FK_ParentTabLineID, FK_SubstituionTabLineID, FK_ParentTabLineCombinationID
            FROM #Src
            WHERE ISNULL(SyncStatus, 'NOT_SYNCED') <> 'DELETE_PENDING'
        )
        MERGE dbo.POS_TablineSubstitutes AS T
        USING UpsertSrc AS S
          ON T.TablineSubstituteID = S.TablineSubstituteID
        WHEN MATCHED THEN
            UPDATE SET
                T.FK_ParentTabLineID            = S.FK_ParentTabLineID,
                T.FK_SubstituionTabLineID       = S.FK_SubstituionTabLineID,
                T.FK_ParentTabLineCombinationID = S.FK_ParentTabLineCombinationID
        WHEN NOT MATCHED BY TARGET THEN
            INSERT (TablineSubstituteID, FK_ParentTabLineID, FK_SubstituionTabLineID, FK_ParentTabLineCombinationID)
            VALUES (S.TablineSubstituteID, S.FK_ParentTabLineID, S.FK_SubstituionTabLineID, S.FK_ParentTabLineCombinationID);

        COMMIT TRAN;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRAN;
        THROW;
    END CATCH
END
GO
