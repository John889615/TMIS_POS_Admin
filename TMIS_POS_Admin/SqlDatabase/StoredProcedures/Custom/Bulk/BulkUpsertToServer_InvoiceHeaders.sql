USE [TMIS_Development]
GO

IF OBJECT_ID('dbo.BulkUpsertToServer_InvoiceHeaders', 'P') IS NOT NULL
    DROP PROCEDURE dbo.BulkUpsertToServer_InvoiceHeaders;
GO

-- =============================================================
-- Updated 2026-05-06 for FOH schema reconciliation (Spec 1):
--   - Drops IsDiscarded; adds IsVoided + audit columns.
--   - Adds FK_CurrencyID, IsPaid, AmountPaid, AmountDue.
--   - Widens PartyName / BookingReference casts 50 -> 100.
--
-- Slot mapping (TVP dbo.BulkInsertToServer):
--   Guid1   InvoiceHeaderID        Bool1   IsPaid
--   Guid2   FK_AccountID           Bool2   IsVoided
--   Int1    FK_LocationID          Date1   DatePaid
--   Int2    FK_CurrencyID          Date2   VoidedDate
--   String1 InvoiceNo (50)         Date3   DateCreated
--   String2 PartyName (100)        Decimal1 DiscountTotal
--   String3 BookingReference (100) Decimal2 GratuityTotal
--   String4 VoidReason (255)       Decimal3 ExclTotal
--   String5 VoidedBy (255)         Decimal4 VatTotal
--   String6 SyncStatus (50)        Decimal5 InclTotal
--                                  Decimal6 AmountPaid
--                                  Decimal7 AmountDue
-- =============================================================
CREATE PROCEDURE dbo.BulkUpsertToServer_InvoiceHeaders
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
                Guid1 AS InvoiceHeaderID,
                Guid2 AS FK_AccountID,
                Int1  AS FK_LocationID,
                Int2  AS FK_CurrencyID,
                CAST(String1 AS VARCHAR(50))  AS InvoiceNo,
                CAST(String2 AS VARCHAR(100)) AS PartyName,
                CAST(String3 AS VARCHAR(100)) AS BookingReference,
                Decimal1 AS DiscountTotal,
                Decimal2 AS GratuityTotal,
                Decimal3 AS ExclTotal,
                Decimal4 AS VatTotal,
                Decimal5 AS InclTotal,
                Decimal6 AS AmountPaid,
                Decimal7 AS AmountDue,
                Bool1  AS IsPaid,
                Date1  AS DatePaid,
                Bool2  AS IsVoided,
                CAST(String4 AS VARCHAR(255)) AS VoidReason,
                Date2  AS VoidedDate,
                CAST(String5 AS VARCHAR(255)) AS VoidedBy,
                Date3  AS DateCreated,
                CAST(String6 AS VARCHAR(50))  AS SyncStatus
            FROM @Rows
            WHERE Guid1 IS NOT NULL
        )
        SELECT * INTO #Src FROM Src;

        IF EXISTS (SELECT 1 FROM #Src WHERE SyncStatus = 'DELETE_PENDING')
        BEGIN
            SELECT DISTINCT InvoiceHeaderID
            INTO #DeleteHeaders
            FROM #Src
            WHERE SyncStatus = 'DELETE_PENDING';

            SELECT DISTINCT InvoiceTabID
            INTO #DeleteTabs
            FROM dbo.POS_InvoiceTabs
            WHERE FK_InvoiceHeaderID IN (SELECT InvoiceHeaderID FROM #DeleteHeaders);

            DELETE il
            FROM dbo.POS_InvoiceLines il
            INNER JOIN #DeleteTabs t
                ON t.InvoiceTabID = il.FK_InvoiceTabID;

            DELETE it
            FROM dbo.POS_InvoiceTabs it
            INNER JOIN #DeleteTabs t
                ON t.InvoiceTabID = it.InvoiceTabID;

            DELETE ip
            FROM dbo.POS_InvoicePayments ip
            INNER JOIN #DeleteHeaders h
                ON h.InvoiceHeaderID = ip.FK_InvoiceID;

            DELETE ih
            FROM dbo.POS_InvoiceHeaders ih
            INNER JOIN #DeleteHeaders h
                ON h.InvoiceHeaderID = ih.InvoiceHeaderID;
        END

        ;WITH UpsertSrc AS
        (
            SELECT InvoiceHeaderID, FK_AccountID, FK_LocationID, FK_CurrencyID,
                   InvoiceNo, PartyName, BookingReference,
                   DiscountTotal, GratuityTotal, ExclTotal, VatTotal, InclTotal,
                   AmountPaid, AmountDue, IsPaid,
                   DatePaid, IsVoided, VoidReason, VoidedDate, VoidedBy, DateCreated
            FROM #Src
            WHERE ISNULL(SyncStatus, 'NOT_SYNCED') <> 'DELETE_PENDING'
        )
        MERGE dbo.POS_InvoiceHeaders AS T
        USING UpsertSrc AS S
          ON T.InvoiceHeaderID = S.InvoiceHeaderID
        WHEN MATCHED THEN
            UPDATE SET
                T.FK_AccountID     = S.FK_AccountID,
                T.FK_LocationID    = S.FK_LocationID,
                T.FK_CurrencyID    = S.FK_CurrencyID,
                T.InvoiceNo        = S.InvoiceNo,
                T.PartyName        = S.PartyName,
                T.BookingReference = S.BookingReference,
                T.DiscountTotal    = S.DiscountTotal,
                T.GratuityTotal    = S.GratuityTotal,
                T.ExclTotal        = S.ExclTotal,
                T.VatTotal         = S.VatTotal,
                T.InclTotal        = S.InclTotal,
                T.AmountPaid       = S.AmountPaid,
                T.AmountDue        = S.AmountDue,
                T.IsPaid           = S.IsPaid,
                T.DatePaid         = S.DatePaid,
                T.IsVoided         = S.IsVoided,
                T.VoidReason       = S.VoidReason,
                T.VoidedDate       = S.VoidedDate,
                T.VoidedBy         = S.VoidedBy,
                T.DateCreated      = S.DateCreated
        WHEN NOT MATCHED BY TARGET THEN
            INSERT (InvoiceHeaderID, FK_AccountID, FK_LocationID, FK_CurrencyID,
                    InvoiceNo, PartyName, BookingReference,
                    DiscountTotal, GratuityTotal, ExclTotal, VatTotal, InclTotal,
                    AmountPaid, AmountDue, IsPaid,
                    DatePaid, IsVoided, VoidReason, VoidedDate, VoidedBy,
                    DateCreated, SyncedToServer)
            VALUES (S.InvoiceHeaderID, S.FK_AccountID, S.FK_LocationID, S.FK_CurrencyID,
                    S.InvoiceNo, S.PartyName, S.BookingReference,
                    S.DiscountTotal, S.GratuityTotal, S.ExclTotal, S.VatTotal, S.InclTotal,
                    S.AmountPaid, S.AmountDue, S.IsPaid,
                    S.DatePaid, S.IsVoided, S.VoidReason, S.VoidedDate, S.VoidedBy,
                    S.DateCreated, 0);

        COMMIT TRAN;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRAN;
        THROW;
    END CATCH
END
GO
