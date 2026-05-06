USE [TMIS_Development]
GO

IF OBJECT_ID('dbo.BulkUpsertToServer_InvoicePayments', 'P') IS NOT NULL
    DROP PROCEDURE dbo.BulkUpsertToServer_InvoicePayments;
GO

-- =============================================================
-- Updated 2026-05-06 for FOH schema reconciliation (Spec 1):
--   - Renames From/To -> Base/Payment columns.
--   - Drops FromTotal / ToTotal.
--   - Adds StaffName, IdempotencyKey, Reference, Notes, IsVoided,
--     VoidReason, VoidedDate, VoidedBy, SignatureBase64.
--   - Tightens NN on FK_InvoiceID, FK_BaseCurrencyID,
--     FK_PaymentCurrencyID, ExchangeRate, ExchangeDate, DatePaid.
--   - Widens ExchangeRate to DECIMAL(18,6).
--
-- IMPORTANT: this SP now uses 9 string slots (String1..String9).
-- If the shared TVP dbo.BulkInsertToServer does not have that many
-- string columns, the TVP must be expanded first. See spec §6.D
-- for the slot-check query.
--
-- Slot mapping:
--   Guid1  InvoicePaymentID        Bool1   IsVoided
--   Guid2  FK_InvoiceID            Date1   ExchangeDate
--   Guid3  IdempotencyKey          Date2   DatePaid
--   Int1   FK_PaymentTypeID        Date3   VoidedDate
--   Int2   FK_BaseCurrencyID       Decimal1 BaseAmountPaid
--   Int3   FK_PaymentCurrencyID    Decimal2 PaymentAmountPaid
--   String1 BaseCurrencyCode       Decimal3 ExchangeRate
--   String2 PaymentCurrencyCode
--   String3 StaffName
--   String4 Reference
--   String5 Notes (MAX)
--   String6 VoidReason
--   String7 VoidedBy
--   String8 SignatureBase64 (MAX)
--   String9 SyncStatus
-- =============================================================
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
                Guid3 AS IdempotencyKey,
                Int1  AS FK_PaymentTypeID,
                Int2  AS FK_BaseCurrencyID,
                Int3  AS FK_PaymentCurrencyID,
                CAST(String1 AS VARCHAR(10))  AS BaseCurrencyCode,
                CAST(String2 AS VARCHAR(10))  AS PaymentCurrencyCode,
                CAST(String3 AS VARCHAR(255)) AS StaffName,
                CAST(String4 AS VARCHAR(100)) AS Reference,
                CAST(String5 AS VARCHAR(MAX)) AS Notes,
                CAST(String6 AS VARCHAR(255)) AS VoidReason,
                CAST(String7 AS VARCHAR(255)) AS VoidedBy,
                CAST(String8 AS VARCHAR(MAX)) AS SignatureBase64,
                CAST(String9 AS VARCHAR(50))  AS SyncStatus,
                Decimal1 AS BaseAmountPaid,
                Decimal2 AS PaymentAmountPaid,
                Decimal3 AS ExchangeRate,
                Date1    AS ExchangeDate,
                Date2    AS DatePaid,
                Date3    AS VoidedDate,
                Bool1    AS IsVoided
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
            SELECT InvoicePaymentID, FK_InvoiceID, IdempotencyKey,
                   FK_PaymentTypeID, FK_BaseCurrencyID, FK_PaymentCurrencyID,
                   BaseCurrencyCode, PaymentCurrencyCode, StaffName,
                   Reference, Notes,
                   BaseAmountPaid, PaymentAmountPaid, ExchangeRate,
                   ExchangeDate, DatePaid,
                   IsVoided, VoidReason, VoidedDate, VoidedBy, SignatureBase64
            FROM #Src
            WHERE ISNULL(SyncStatus, 'NOT_SYNCED') <> 'DELETE_PENDING'
        )
        MERGE dbo.POS_InvoicePayments AS T
        USING UpsertSrc AS S
          ON T.InvoicePaymentID = S.InvoicePaymentID
        WHEN MATCHED THEN
            UPDATE SET
                T.FK_InvoiceID         = S.FK_InvoiceID,
                T.IdempotencyKey       = S.IdempotencyKey,
                T.FK_PaymentTypeID     = S.FK_PaymentTypeID,
                T.FK_BaseCurrencyID    = S.FK_BaseCurrencyID,
                T.FK_PaymentCurrencyID = S.FK_PaymentCurrencyID,
                T.BaseCurrencyCode     = S.BaseCurrencyCode,
                T.PaymentCurrencyCode  = S.PaymentCurrencyCode,
                T.StaffName            = S.StaffName,
                T.Reference            = S.Reference,
                T.Notes                = S.Notes,
                T.BaseAmountPaid       = S.BaseAmountPaid,
                T.PaymentAmountPaid    = S.PaymentAmountPaid,
                T.ExchangeRate         = S.ExchangeRate,
                T.ExchangeDate         = S.ExchangeDate,
                T.DatePaid             = S.DatePaid,
                T.IsVoided             = S.IsVoided,
                T.VoidReason           = S.VoidReason,
                T.VoidedDate           = S.VoidedDate,
                T.VoidedBy             = S.VoidedBy,
                T.SignatureBase64      = S.SignatureBase64
        WHEN NOT MATCHED BY TARGET THEN
            INSERT (InvoicePaymentID, FK_InvoiceID, IdempotencyKey,
                    FK_PaymentTypeID, FK_BaseCurrencyID, FK_PaymentCurrencyID,
                    BaseCurrencyCode, PaymentCurrencyCode, StaffName,
                    Reference, Notes,
                    BaseAmountPaid, PaymentAmountPaid, ExchangeRate,
                    ExchangeDate, DatePaid,
                    IsVoided, VoidReason, VoidedDate, VoidedBy, SignatureBase64)
            VALUES (S.InvoicePaymentID, S.FK_InvoiceID, S.IdempotencyKey,
                    S.FK_PaymentTypeID, S.FK_BaseCurrencyID, S.FK_PaymentCurrencyID,
                    S.BaseCurrencyCode, S.PaymentCurrencyCode, S.StaffName,
                    S.Reference, S.Notes,
                    S.BaseAmountPaid, S.PaymentAmountPaid, S.ExchangeRate,
                    S.ExchangeDate, S.DatePaid,
                    S.IsVoided, S.VoidReason, S.VoidedDate, S.VoidedBy, S.SignatureBase64);

        COMMIT TRAN;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRAN;
        THROW;
    END CATCH
END
GO
