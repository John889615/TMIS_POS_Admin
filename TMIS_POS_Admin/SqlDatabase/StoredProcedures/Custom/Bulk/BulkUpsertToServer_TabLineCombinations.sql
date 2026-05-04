USE [TMIS_BlueSafaris]
GO

IF OBJECT_ID('dbo.BulkUpsertToServer_TabLineCombinations', 'P') IS NOT NULL
    DROP PROCEDURE dbo.BulkUpsertToServer_TabLineCombinations;
GO

CREATE PROCEDURE dbo.BulkUpsertToServer_TabLineCombinations
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
                Guid1 AS TabLineCombinationID,
                Guid2 AS FK_TabLineID,
                Int1 AS FK_ProductCombinationID,
                CAST(String1 AS VARCHAR(255)) AS Product,
                Bool1 AS Hold,
                CAST(String2 AS VARCHAR(MAX)) AS Notes,
                CAST(String3 AS VARCHAR(50)) AS SyncStatus
            FROM @Rows
            WHERE Guid1 IS NOT NULL
        )
        SELECT * INTO #Src FROM Src;

        IF EXISTS (SELECT 1 FROM #Src WHERE SyncStatus = 'DELETE_PENDING')
        BEGIN
            SELECT DISTINCT TabLineCombinationID
            INTO #DeleteCombinations
            FROM #Src
            WHERE SyncStatus = 'DELETE_PENDING';

            DELETE tpm
            FROM dbo.POS_TabLinePreparationMethods tpm
            INNER JOIN #DeleteCombinations x
                ON x.TabLineCombinationID = tpm.FK_TabLineCombinationID;

            DELETE ts
            FROM dbo.POS_TablineSubstitutes ts
            INNER JOIN #DeleteCombinations x
                ON x.TabLineCombinationID = ts.FK_ParentTabLineCombinationID;

            DELETE tlc
            FROM dbo.POS_TabLineCombinations tlc
            INNER JOIN #DeleteCombinations x
                ON x.TabLineCombinationID = tlc.TabLineCombinationID;
        END

        ;WITH UpsertSrc AS
        (
            SELECT TabLineCombinationID, FK_TabLineID, FK_ProductCombinationID, Product, Hold, Notes
            FROM #Src
            WHERE ISNULL(SyncStatus, 'NOT_SYNCED') <> 'DELETE_PENDING'
        )
        MERGE dbo.POS_TabLineCombinations AS T
        USING UpsertSrc AS S
          ON T.TabLineCombinationID = S.TabLineCombinationID
        WHEN MATCHED THEN
            UPDATE SET
                T.FK_TabLineID = S.FK_TabLineID,
                T.FK_ProductCombinationID = S.FK_ProductCombinationID,
                T.Product = S.Product,
                T.Hold = S.Hold,
                T.Notes = S.Notes
        WHEN NOT MATCHED BY TARGET THEN
            INSERT (TabLineCombinationID, FK_TabLineID, FK_ProductCombinationID, Product, Hold, Notes)
            VALUES (S.TabLineCombinationID, S.FK_TabLineID, S.FK_ProductCombinationID, S.Product, S.Hold, S.Notes);

        COMMIT TRAN;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRAN;
        THROW;
    END CATCH
END
GO