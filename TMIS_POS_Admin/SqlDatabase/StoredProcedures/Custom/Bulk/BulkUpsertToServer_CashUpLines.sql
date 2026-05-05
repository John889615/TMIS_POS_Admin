USE [TMIS_BlueSafaris]
GO

IF OBJECT_ID('dbo.BulkUpsertToServer_CashUpLines', 'P') IS NOT NULL
    DROP PROCEDURE dbo.BulkUpsertToServer_CashUpLines;
GO

CREATE PROCEDURE dbo.BulkUpsertToServer_CashUpLines
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
                Guid1 AS CashUpPaymentTypeID,
                Guid2 AS FK_CashUpID,
                Int1 AS FK_PaymentTypeID,
                Decimal1 AS SystemAmount,
                Decimal2 AS CountedAmount,
                Decimal3 AS VarianceAmount,
                CAST(String1 AS VARCHAR(MAX)) AS Notes,
                Date1 AS DateCreated,
                Date2 AS DateUpdated,
                CAST(String2 AS VARCHAR(50)) AS SyncStatus
            FROM @Rows
            WHERE Guid1 IS NOT NULL
        )
        SELECT * INTO #Src FROM Src;

        DELETE cl
        FROM dbo.POS_CashUpLines cl
        INNER JOIN #Src s
            ON s.CashUpPaymentTypeID = cl.CashUpPaymentTypeID
        WHERE s.SyncStatus = 'DELETE_PENDING';

        ;WITH UpsertSrc AS
        (
            SELECT CashUpPaymentTypeID, FK_CashUpID, FK_PaymentTypeID, SystemAmount, CountedAmount, VarianceAmount, Notes, DateCreated, DateUpdated
            FROM #Src
            WHERE ISNULL(SyncStatus, 'NOT_SYNCED') <> 'DELETE_PENDING'
        )
        MERGE dbo.POS_CashUpLines AS T
        USING UpsertSrc AS S
          ON T.CashUpPaymentTypeID = S.CashUpPaymentTypeID
        WHEN MATCHED THEN
            UPDATE SET
                T.FK_CashUpID = S.FK_CashUpID,
                T.FK_PaymentTypeID = S.FK_PaymentTypeID,
                T.SystemAmount = S.SystemAmount,
                T.CountedAmount = S.CountedAmount,
                T.VarianceAmount = S.VarianceAmount,
                T.Notes = S.Notes,
                T.DateCreated = S.DateCreated,
                T.DateUpdated = S.DateUpdated
        WHEN NOT MATCHED BY TARGET THEN
            INSERT (CashUpPaymentTypeID, FK_CashUpID, FK_PaymentTypeID, SystemAmount, CountedAmount, VarianceAmount, Notes, DateCreated, DateUpdated)
            VALUES (S.CashUpPaymentTypeID, S.FK_CashUpID, S.FK_PaymentTypeID, S.SystemAmount, S.CountedAmount, S.VarianceAmount, S.Notes, S.DateCreated, S.DateUpdated);

        COMMIT TRAN;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRAN;
        THROW;
    END CATCH
END
GO