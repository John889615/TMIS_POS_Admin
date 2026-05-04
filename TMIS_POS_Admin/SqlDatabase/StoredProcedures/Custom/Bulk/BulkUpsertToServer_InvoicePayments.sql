USE [TMIS_BlueSafaris]
GO

IF OBJECT_ID('dbo.BulkUpsertToServer_InvoicePayments', 'P') IS NOT NULL
    DROP PROCEDURE dbo.BulkUpsertToServer_InvoicePayments;
GO

CREATE PROCEDURE dbo.BulkUpsertToServer_InvoicePayments
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
                Guid1 AS InvoicePaymentID,
                Guid2 AS FK_InvoiceID,
                Int1 AS FK_PaymentTypeID,
                Int2 AS FK_FromCurrencyID,
                Int3 AS FK_ToCurrencyID,
                CAST(String1 AS VARCHAR(10)) AS FromCurrency,
                CAST(String2 AS VARCHAR(10)) AS ToCurrency,
                Decimal1 AS FromTotal,
                Decimal2 AS ToTotal,
                Decimal3 AS FromAmountPaid,
                Decimal4 AS ToAmountPaid,
                Decimal5 AS ExchangeRate,
                Date1 AS ExchangeDate,
                Date2 AS DatePaid,
                CAST(String3 AS VARCHAR(50)) AS SyncStatus
            FROM @Rows
            WHERE Guid1 IS NOT NULL
        )
        SELECT * INTO #Src FROM Src;

        DELETE ip
        FROM dbo.POS_InvoicePayments ip
        INNER JOIN #Src s
            ON s.InvoicePaymentID = ip.InvoicePaymentID
        WHERE s.SyncStatus = 'DELETE_PENDING';

        ;WITH UpsertSrc AS
        (
            SELECT InvoicePaymentID, FK_InvoiceID, FK_PaymentTypeID, FK_FromCurrencyID, FK_ToCurrencyID, FromCurrency, ToCurrency, FromTotal, ToTotal, FromAmountPaid, ToAmountPaid, ExchangeRate, ExchangeDate, DatePaid
            FROM #Src
            WHERE ISNULL(SyncStatus, 'NOT_SYNCED') <> 'DELETE_PENDING'
        )
        MERGE dbo.POS_InvoicePayments AS T
        USING UpsertSrc AS S
          ON T.InvoicePaymentID = S.InvoicePaymentID
        WHEN MATCHED THEN
            UPDATE SET
                T.FK_InvoiceID = S.FK_InvoiceID,
                T.FK_PaymentTypeID = S.FK_PaymentTypeID,
                T.FK_FromCurrencyID = S.FK_FromCurrencyID,
                T.FK_ToCurrencyID = S.FK_ToCurrencyID,
                T.FromCurrency = S.FromCurrency,
                T.ToCurrency = S.ToCurrency,
                T.FromTotal = S.FromTotal,
                T.ToTotal = S.ToTotal,
                T.FromAmountPaid = S.FromAmountPaid,
                T.ToAmountPaid = S.ToAmountPaid,
                T.ExchangeRate = S.ExchangeRate,
                T.ExchangeDate = S.ExchangeDate,
                T.DatePaid = S.DatePaid
        WHEN NOT MATCHED BY TARGET THEN
            INSERT (InvoicePaymentID, FK_InvoiceID, FK_PaymentTypeID, FK_FromCurrencyID, FK_ToCurrencyID, FromCurrency, ToCurrency, FromTotal, ToTotal, FromAmountPaid, ToAmountPaid, ExchangeRate, ExchangeDate, DatePaid)
            VALUES (S.InvoicePaymentID, S.FK_InvoiceID, S.FK_PaymentTypeID, S.FK_FromCurrencyID, S.FK_ToCurrencyID, S.FromCurrency, S.ToCurrency, S.FromTotal, S.ToTotal, S.FromAmountPaid, S.ToAmountPaid, S.ExchangeRate, S.ExchangeDate, S.DatePaid);

        COMMIT TRAN;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRAN;
        THROW;
    END CATCH
END
GO