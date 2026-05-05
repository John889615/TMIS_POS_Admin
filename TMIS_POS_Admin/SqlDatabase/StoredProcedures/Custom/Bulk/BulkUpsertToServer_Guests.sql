USE [TMIS_Development]
GO

IF OBJECT_ID('dbo.BulkUpsertToServer_Guests', 'P') IS NOT NULL
    DROP PROCEDURE dbo.BulkUpsertToServer_Guests;
GO

CREATE PROCEDURE dbo.BulkUpsertToServer_Guests
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
                Int1 AS GuestID,
                CAST(String1 AS VARCHAR(10)) AS Title,
                CAST(String2 AS VARCHAR(50)) AS FirstName,
                CAST(String3 AS VARCHAR(50)) AS MiddleName,
                CAST(String4 AS VARCHAR(50)) AS LastName,
                CAST(Date1 AS DATE) AS DateOfBirth,
                CAST(String5 AS VARCHAR(20)) AS Gender,
                CAST(String6 AS VARCHAR(50)) AS Nationality,
                CAST(String7 AS VARCHAR(20)) AS PreferredLanguage,
                CAST(String8 AS VARCHAR(MAX)) AS SpecialRequests,
                CAST(String9 AS VARCHAR(50)) AS LoyaltyNumber,
                CAST(String10 AS VARCHAR(MAX)) AS Notes,
                Date2 AS DateCreated,
                Date3 AS DateUpdated,
                CAST(String11 AS VARCHAR(50)) AS SyncStatus
            FROM @Rows
            WHERE Int1 IS NOT NULL
        )
        SELECT *
        INTO #Src
        FROM Src;

        IF EXISTS (SELECT 1 FROM #Src WHERE SyncStatus = 'DELETE_PENDING')
        BEGIN
            SELECT DISTINCT GuestID
            INTO #DeleteGuests
            FROM #Src
            WHERE SyncStatus = 'DELETE_PENDING';

            SELECT DISTINCT a.AccountID
            INTO #DeleteAccounts
            FROM dbo.POS_Accounts a
            INNER JOIN #DeleteGuests g
                ON g.GuestID = a.FK_ResponsibleID;

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

            DELETE a
            FROM dbo.POS_Accounts a
            INNER JOIN #DeleteAccounts x
                ON x.AccountID = a.AccountID;

            DELETE ar
            FROM dbo.POS_Arrivals ar
            INNER JOIN #DeleteGuests g
                ON g.GuestID = ar.FK_GuestID;

            DELETE bg
            FROM dbo.BookingGuests bg
            INNER JOIN #DeleteGuests g
                ON g.GuestID = bg.FK_GuestID;

            DELETE ag
            FROM dbo.POS_AccountGuests ag
            INNER JOIN #DeleteGuests g
                ON g.GuestID = ag.FK_GuestID;

            DELETE g
            FROM dbo.Guests g
            INNER JOIN #DeleteGuests x
                ON x.GuestID = g.GuestID;
        END

        SELECT
            GuestID,
            Title,
            FirstName,
            MiddleName,
            LastName,
            DateOfBirth,
            Gender,
            Nationality,
            PreferredLanguage,
            SpecialRequests,
            LoyaltyNumber,
            Notes,
            DateCreated,
            DateUpdated
        INTO #UpsertSrc
        FROM #Src
        WHERE ISNULL(SyncStatus, 'NOT_SYNCED') <> 'DELETE_PENDING';

        SET IDENTITY_INSERT dbo.Guests ON;

        MERGE dbo.Guests AS T
        USING #UpsertSrc AS S
          ON T.GuestID = S.GuestID
        WHEN MATCHED THEN
            UPDATE SET
                T.Title = S.Title,
                T.FirstName = S.FirstName,
                T.MiddleName = S.MiddleName,
                T.LastName = S.LastName,
                T.DateOfBirth = S.DateOfBirth,
                T.Gender = S.Gender,
                T.Nationality = S.Nationality,
                T.PreferredLanguage = S.PreferredLanguage,
                T.SpecialRequests = S.SpecialRequests,
                T.LoyaltyNumber = S.LoyaltyNumber,
                T.Notes = S.Notes,
                T.DateCreated = S.DateCreated,
                T.DateUpdated = S.DateUpdated
        WHEN NOT MATCHED BY TARGET THEN
            INSERT
            (
                GuestID,
                Title,
                FirstName,
                MiddleName,
                LastName,
                DateOfBirth,
                Gender,
                Nationality,
                PreferredLanguage,
                SpecialRequests,
                LoyaltyNumber,
                Notes,
                DateCreated,
                DateUpdated
            )
            VALUES
            (
                S.GuestID,
                S.Title,
                S.FirstName,
                S.MiddleName,
                S.LastName,
                S.DateOfBirth,
                S.Gender,
                S.Nationality,
                S.PreferredLanguage,
                S.SpecialRequests,
                S.LoyaltyNumber,
                S.Notes,
                S.DateCreated,
                S.DateUpdated
            );

        SET IDENTITY_INSERT dbo.Guests OFF;

        COMMIT TRAN;
    END TRY
    BEGIN CATCH
        DECLARE @ErrorMessage NVARCHAR(4000);
        DECLARE @ErrorSeverity INT;
        DECLARE @ErrorState INT;

        SELECT
            @ErrorMessage = ERROR_MESSAGE(),
            @ErrorSeverity = ERROR_SEVERITY(),
            @ErrorState = ERROR_STATE();

        IF @@TRANCOUNT > 0
            ROLLBACK TRAN;

        BEGIN TRY
            SET IDENTITY_INSERT dbo.Guests OFF;
        END TRY
        BEGIN CATCH
        END CATCH

        RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END
GO