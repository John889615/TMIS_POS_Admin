USE [TMIS_Development]
GO

IF OBJECT_ID('dbo.BulkUpsertToServer_InvoiceHeaders', 'P') IS NOT NULL
    DROP PROCEDURE dbo.BulkUpsertToServer_InvoiceHeaders;
GO

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
                Int1 AS FK_LocationID,
                CAST(String1 AS VARCHAR(50)) AS InvoiceNo,
                CAST(String2 AS VARCHAR(50)) AS PartyName,
                CAST(String3 AS VARCHAR(50)) AS BookingReference,
                Decimal1 AS DiscountTotal,
                Decimal2 AS GratuityTotal,
                Decimal3 AS ExclTotal,
                Decimal4 AS VatTotal,
                Decimal5 AS InclTotal,
                Bool1 AS IsDiscarded,
                Date1 AS DateCreated,
                Date2 AS DatePaid,
                CAST(String4 AS VARCHAR(50)) AS SyncStatus
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
            SELECT InvoiceHeaderID, FK_AccountID, FK_LocationID, InvoiceNo, PartyName, BookingReference, DiscountTotal, GratuityTotal, ExclTotal, VatTotal, InclTotal, IsDiscarded, DateCreated, DatePaid
            FROM #Src
            WHERE ISNULL(SyncStatus, 'NOT_SYNCED') <> 'DELETE_PENDING'
        )
        MERGE dbo.POS_InvoiceHeaders AS T
        USING UpsertSrc AS S
          ON T.InvoiceHeaderID = S.InvoiceHeaderID
        WHEN MATCHED THEN
            UPDATE SET
                T.FK_AccountID = S.FK_AccountID,
                T.FK_LocationID = S.FK_LocationID,
                T.InvoiceNo = S.InvoiceNo,
                T.PartyName = S.PartyName,
                T.BookingReference = S.BookingReference,
                T.DiscountTotal = S.DiscountTotal,
                T.GratuityTotal = S.GratuityTotal,
                T.ExclTotal = S.ExclTotal,
                T.VatTotal = S.VatTotal,
                T.InclTotal = S.InclTotal,
                T.IsDiscarded = S.IsDiscarded,
                T.DateCreated = S.DateCreated,
                T.DatePaid = S.DatePaid
        WHEN NOT MATCHED BY TARGET THEN
            INSERT (InvoiceHeaderID, FK_AccountID, FK_LocationID, InvoiceNo, PartyName, BookingReference, DiscountTotal, GratuityTotal, ExclTotal, VatTotal, InclTotal, IsDiscarded, DateCreated, DatePaid, SyncedToServer)
            VALUES (S.InvoiceHeaderID, S.FK_AccountID, S.FK_LocationID, S.InvoiceNo, S.PartyName, S.BookingReference, S.DiscountTotal, S.GratuityTotal, S.ExclTotal, S.VatTotal, S.InclTotal, S.IsDiscarded, S.DateCreated, S.DatePaid, 0);

        COMMIT TRAN;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRAN;
        THROW;
    END CATCH
END
GO