USE [TMIS_BlueSafaris]
GO

IF OBJECT_ID('dbo.BulkUpsertToServer_TabLineExtras', 'P') IS NOT NULL
    DROP PROCEDURE dbo.BulkUpsertToServer_TabLineExtras;
GO

CREATE PROCEDURE dbo.BulkUpsertToServer_TabLineExtras
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
                Guid1 AS TabLineExtraID,
                Guid2 AS FK_TabLineID,
                Int1 AS FK_ProductID,
                CAST(String1 AS VARCHAR(255)) AS Product,
                CAST(String2 AS VARCHAR(50)) AS SyncStatus
            FROM @Rows
            WHERE Guid1 IS NOT NULL
        )
        SELECT * INTO #Src FROM Src;

        DELETE tle
        FROM dbo.POS_TabLineExtras tle
        INNER JOIN #Src s
            ON s.TabLineExtraID = tle.TabLineExtraID
        WHERE s.SyncStatus = 'DELETE_PENDING';

        ;WITH UpsertSrc AS
        (
            SELECT TabLineExtraID, FK_TabLineID, FK_ProductID, Product
            FROM #Src
            WHERE ISNULL(SyncStatus, 'NOT_SYNCED') <> 'DELETE_PENDING'
        )
        MERGE dbo.POS_TabLineExtras AS T
        USING UpsertSrc AS S
          ON T.TabLineExtraID = S.TabLineExtraID
        WHEN MATCHED THEN
            UPDATE SET
                T.FK_TabLineID = S.FK_TabLineID,
                T.FK_ProductID = S.FK_ProductID,
                T.Product = S.Product
        WHEN NOT MATCHED BY TARGET THEN
            INSERT (TabLineExtraID, FK_TabLineID, FK_ProductID, Product)
            VALUES (S.TabLineExtraID, S.FK_TabLineID, S.FK_ProductID, S.Product);

        COMMIT TRAN;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRAN;
        THROW;
    END CATCH
END
GO