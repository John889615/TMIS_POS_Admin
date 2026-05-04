USE [TMIS_BlueSafaris]
GO

IF OBJECT_ID('dbo.BulkUpsertToServer_BookingHeaders', 'P') IS NOT NULL
    DROP PROCEDURE dbo.BulkUpsertToServer_BookingHeaders;
GO

CREATE PROCEDURE dbo.BulkUpsertToServer_BookingHeaders
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
                Int1 AS BookingHeaderID,
                CAST(String1 AS VARCHAR(150)) AS PartyName,
                CAST(String2 AS VARCHAR(50)) AS BookingReference,
                CAST(Date1 AS DATE) AS TravelStart,
                CAST(Date2 AS DATE) AS TravelEnd,
                Date3 AS DateCreated,
                Date4 AS DateUpdated,
                CAST(Bool1 AS BIT) AS IsStaffBooking,
                CAST(String3 AS VARCHAR(50)) AS SyncStatus
            FROM @Rows
            WHERE Int1 IS NOT NULL
        )
        SELECT *
        INTO #Src
        FROM Src;

        /* =========================================================
           DELETE_PENDING
           Only delete rows and their dependent chain for the
           booking headers in this batch
           ========================================================= */

        IF EXISTS (SELECT 1 FROM #Src WHERE SyncStatus = 'DELETE_PENDING')
        BEGIN
            /* Cache all IDs tied to the BookingHeaders being deleted */
            SELECT DISTINCT s.BookingHeaderID
            INTO #DeleteBookingHeaders
            FROM #Src s
            WHERE s.SyncStatus = 'DELETE_PENDING';

            SELECT DISTINCT a.AccountID
            INTO #DeleteAccounts
            FROM dbo.POS_Accounts a
            INNER JOIN #DeleteBookingHeaders d
                ON d.BookingHeaderID = a.FK_BookingHeaderID;

            SELECT DISTINCT t.TabID
            INTO #DeleteTabs
            FROM dbo.POS_Tabs t
            INNER JOIN #DeleteAccounts a
                ON a.AccountID = t.FK_AccountID;

            SELECT DISTINCT tl.TabLineID
            INTO #DeleteTabLines
            FROM dbo.POS_TabLines tl
            INNER JOIN #DeleteTabs t
                ON t.TabID = tl.FK_TabID;

            SELECT DISTINCT tlc.TabLineCombinationID
            INTO #DeleteTabLineCombinations
            FROM dbo.POS_TabLineCombinations tlc
            INNER JOIN #DeleteTabLines tl
                ON tl.TabLineID = tlc.FK_TabLineID;

            SELECT DISTINCT ih.InvoiceHeaderID
            INTO #DeleteInvoiceHeaders
            FROM dbo.POS_InvoiceHeaders ih
            INNER JOIN #DeleteAccounts a
                ON a.AccountID = ih.FK_AccountID;

            SELECT DISTINCT it.InvoiceTabID
            INTO #DeleteInvoiceTabs
            FROM dbo.POS_InvoiceTabs it
            INNER JOIN #DeleteInvoiceHeaders ih
                ON ih.InvoiceHeaderID = it.FK_InvoiceHeaderID;

            /* Deepest children first */

            DELETE tpm
            FROM dbo.POS_TabLinePreparationMethods tpm
            INNER JOIN #DeleteTabLineCombinations x
                ON x.TabLineCombinationID = tpm.FK_TabLineCombinationID;

            DELETE ts
            FROM dbo.POS_TablineSubstitutes ts
            LEFT JOIN #DeleteTabLines tl1
                ON tl1.TabLineID = ts.FK_ParentTabLineID
            LEFT JOIN #DeleteTabLines tl2
                ON tl2.TabLineID = ts.FK_SubstituionTabLineID
            LEFT JOIN #DeleteTabLineCombinations tlc
                ON tlc.TabLineCombinationID = ts.FK_ParentTabLineCombinationID
            WHERE tl1.TabLineID IS NOT NULL
               OR tl2.TabLineID IS NOT NULL
               OR tlc.TabLineCombinationID IS NOT NULL;

            DELETE tle
            FROM dbo.POS_TabLineExtras tle
            INNER JOIN #DeleteTabLines x
                ON x.TabLineID = tle.FK_TabLineID;

            DELETE tlg
            FROM dbo.POS_TabLineGuests tlg
            INNER JOIN #DeleteTabLines x
                ON x.TabLineID = tlg.FK_TabLineID;

            DELETE vl
            FROM dbo.POS_VoidLogs vl
            LEFT JOIN #DeleteTabs t
                ON t.TabID = vl.FK_TabID
            LEFT JOIN #DeleteTabLines tl
                ON tl.TabLineID = vl.FK_TabLineID
            WHERE t.TabID IS NOT NULL
               OR tl.TabLineID IS NOT NULL;

            DELETE il
            FROM dbo.POS_InvoiceLines il
            INNER JOIN #DeleteInvoiceTabs x
                ON x.InvoiceTabID = il.FK_InvoiceTabID;

            /* Next layer */

            DELETE tlc
            FROM dbo.POS_TabLineCombinations tlc
            INNER JOIN #DeleteTabLineCombinations x
                ON x.TabLineCombinationID = tlc.TabLineCombinationID;

            DELETE it
            FROM dbo.POS_InvoiceTabs it
            INNER JOIN #DeleteInvoiceTabs x
                ON x.InvoiceTabID = it.InvoiceTabID;

            DELETE ip
            FROM dbo.POS_InvoicePayments ip
            INNER JOIN #DeleteInvoiceHeaders x
                ON x.InvoiceHeaderID = ip.FK_InvoiceID;

            DELETE tl
            FROM dbo.POS_TabLines tl
            INNER JOIN #DeleteTabLines x
                ON x.TabLineID = tl.TabLineID;

            DELETE t
            FROM dbo.POS_Tabs t
            INNER JOIN #DeleteTabs x
                ON x.TabID = t.TabID;

            DELETE ag
            FROM dbo.POS_AccountGuests ag
            INNER JOIN #DeleteAccounts x
                ON x.AccountID = ag.FK_AccountID;

            DELETE ih
            FROM dbo.POS_InvoiceHeaders ih
            INNER JOIN #DeleteInvoiceHeaders x
                ON x.InvoiceHeaderID = ih.InvoiceHeaderID;

            DELETE bg
            FROM dbo.BookingGuests bg
            INNER JOIN #DeleteBookingHeaders x
                ON x.BookingHeaderID = bg.FK_BookingHeaderID;

            DELETE a
            FROM dbo.POS_Accounts a
            INNER JOIN #DeleteAccounts x
                ON x.AccountID = a.AccountID;

            DELETE bh
            FROM dbo.BookingHeaders bh
            INNER JOIN #DeleteBookingHeaders x
                ON x.BookingHeaderID = bh.BookingHeaderID;
        END

        /* =========================================================
           UPSERT rows that are not DELETE_PENDING
           ========================================================= */

        ;WITH UpsertSrc AS
        (
            SELECT
                BookingHeaderID,
                PartyName,
                BookingReference,
                TravelStart,
                TravelEnd,
                DateCreated,
                DateUpdated,
                IsStaffBooking
            FROM #Src
            WHERE ISNULL(SyncStatus, 'NOT_SYNCED') <> 'DELETE_PENDING'
        )
        MERGE dbo.BookingHeaders AS T
        USING UpsertSrc AS S
          ON T.BookingHeaderID = S.BookingHeaderID
        WHEN MATCHED THEN
            UPDATE SET
                T.PartyName = S.PartyName,
                T.BookingReference = S.BookingReference,
                T.TravelStart = S.TravelStart,
                T.TravelEnd = S.TravelEnd,
                T.DateCreated = S.DateCreated,
                T.DateUpdated = S.DateUpdated,
                T.IsStaffBooking = S.IsStaffBooking
        WHEN NOT MATCHED BY TARGET THEN
            INSERT
            (
                BookingHeaderID,
                PartyName,
                BookingReference,
                TravelStart,
                TravelEnd,
                DateCreated,
                DateUpdated,
                IsStaffBooking
            )
            VALUES
            (
                S.BookingHeaderID,
                S.PartyName,
                S.BookingReference,
                S.TravelStart,
                S.TravelEnd,
                S.DateCreated,
                S.DateUpdated,
                S.IsStaffBooking
            );

        COMMIT TRAN;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRAN;

        THROW;
    END CATCH
END
GO