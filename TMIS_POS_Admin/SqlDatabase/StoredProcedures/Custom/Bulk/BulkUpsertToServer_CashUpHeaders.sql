USE [TMIS_BlueSafaris]
GO

IF OBJECT_ID('dbo.BulkUpsertToServer_CashUpHeaders', 'P') IS NOT NULL
    DROP PROCEDURE dbo.BulkUpsertToServer_CashUpHeaders;
GO

CREATE PROCEDURE dbo.BulkUpsertToServer_CashUpHeaders
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
                Guid1 AS CashUpHeaderID,
                Int1 AS FK_CostCenterID,
                Int2 AS FK_CurrencyID,
                CAST(Date1 AS DATE) AS CashUpDate,
                String2 AS CashUpBy,
                Decimal1 AS TotalSystemAmount,
                Decimal2 AS TotalCountedAmount,
                Decimal3 AS TotalVariance,
                CAST(String1 AS VARCHAR(MAX)) AS Notes,
                Bool1 AS IsFinalised,
                Date2 AS DateCreated,
                Date3 AS DateUpdated,
                CAST(String3 AS VARCHAR(50)) AS SyncStatus
            FROM @Rows
            WHERE Guid1 IS NOT NULL
        )
        SELECT * INTO #Src FROM Src;

        IF EXISTS (SELECT 1 FROM #Src WHERE SyncStatus = 'DELETE_PENDING')
        BEGIN
            SELECT DISTINCT CashUpHeaderID
            INTO #DeleteHeaders
            FROM #Src
            WHERE SyncStatus = 'DELETE_PENDING';

            DELETE cl
            FROM dbo.POS_CashUpLines cl
            INNER JOIN #DeleteHeaders x
                ON x.CashUpHeaderID = cl.FK_CashUpID;

            DELETE ch
            FROM dbo.POS_CashUpHeaders ch
            INNER JOIN #DeleteHeaders x
                ON x.CashUpHeaderID = ch.CashUpHeaderID;
        END

        ;WITH UpsertSrc AS
        (
            SELECT CashUpHeaderID, FK_CostCenterID, FK_CurrencyID, CashUpDate, CashUpBy, TotalSystemAmount, TotalCountedAmount, TotalVariance, Notes, IsFinalised, DateCreated, DateUpdated
            FROM #Src
            WHERE ISNULL(SyncStatus, 'NOT_SYNCED') <> 'DELETE_PENDING'
        )
        MERGE dbo.POS_CashUpHeaders AS T
        USING UpsertSrc AS S
          ON T.CashUpHeaderID = S.CashUpHeaderID
        WHEN MATCHED THEN
            UPDATE SET
                T.FK_CostCenterID = S.FK_CostCenterID,
                T.FK_CurrencyID = S.FK_CurrencyID,
                T.CashUpDate = S.CashUpDate,
                T.CashUpBy = S.CashUpBy,
                T.TotalSystemAmount = S.TotalSystemAmount,
                T.TotalCountedAmount = S.TotalCountedAmount,
                T.TotalVariance = S.TotalVariance,
                T.Notes = S.Notes,
                T.IsFinalised = S.IsFinalised,
                T.DateCreated = S.DateCreated,
                T.DateUpdated = S.DateUpdated
        WHEN NOT MATCHED BY TARGET THEN
            INSERT (CashUpHeaderID, FK_CostCenterID, FK_CurrencyID, CashUpDate, CashUpBy, TotalSystemAmount, TotalCountedAmount, TotalVariance, Notes, IsFinalised, DateCreated, DateUpdated)
            VALUES (S.CashUpHeaderID, S.FK_CostCenterID, S.FK_CurrencyID, S.CashUpDate, S.CashUpBy, S.TotalSystemAmount, S.TotalCountedAmount, S.TotalVariance, S.Notes, S.IsFinalised, S.DateCreated, S.DateUpdated);

        COMMIT TRAN;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRAN;
        THROW;
    END CATCH
END
GO