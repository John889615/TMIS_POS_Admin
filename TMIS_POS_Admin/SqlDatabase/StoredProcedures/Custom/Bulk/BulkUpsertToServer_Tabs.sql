USE [TMIS_BlueSafaris]
GO

IF OBJECT_ID('dbo.BulkUpsertToServer_Tabs', 'P') IS NOT NULL
    DROP PROCEDURE dbo.BulkUpsertToServer_Tabs;
GO

CREATE PROCEDURE dbo.BulkUpsertToServer_Tabs
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
                Guid1 AS TabID,
                Int1 AS FK_LocationID,
                String4 AS CreatedBy,
                Guid2 AS FK_AccountID,
                Int3 AS FK_CostCenterID,
                Int4 AS FK_PaymentTypeID,
                CAST(String1 AS VARCHAR(50)) AS TabName,
                Int5 AS TableName,
                Int6 AS NoOfGuests,
                Decimal1 AS Gratuity,
                Int7 AS GratuityPerc,
                Decimal2 AS Discount,
                Int8 AS DiscountPerc,
                Bool1 AS IsVoided,
                CAST(String2 AS VARCHAR(MAX)) AS VoidNote,
                Bool2 AS IsPaid,
                Decimal3 AS AmountPaid,
                Decimal4 AS AmountDue,
                Decimal5 AS VatTotal,
                Date1 AS PaymentDate,
                Date2 AS ClosedDate,
                CAST(String3 AS VARCHAR(MAX)) AS AdditionalInfo,
                Date3 AS DateCreated,
                Date4 AS DateUpdated,
                Int10 AS FK_CurrencyID,
                Decimal6 AS CurrentExchangeRate,
                CAST(String5 AS VARCHAR(50)) AS SyncStatus
            FROM @Rows
            WHERE Guid1 IS NOT NULL
        )
        SELECT * INTO #Src FROM Src;

        IF EXISTS (SELECT 1 FROM #Src WHERE SyncStatus = 'DELETE_PENDING')
        BEGIN
            SELECT DISTINCT TabID
            INTO #DeleteTabs
            FROM #Src
            WHERE SyncStatus = 'DELETE_PENDING';

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

            SELECT DISTINCT it.InvoiceTabID
            INTO #DeleteInvoiceTabs
            FROM dbo.POS_InvoiceTabs it
            INNER JOIN #DeleteTabs t
                ON t.TabID = it.FK_TabID;

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

            DELETE tl
            FROM dbo.POS_TabLines tl
            INNER JOIN #DeleteTabLines x
                ON x.TabLineID = tl.TabLineID;

            DELETE t
            FROM dbo.POS_Tabs t
            INNER JOIN #DeleteTabs x
                ON x.TabID = t.TabID;
        END

        ;WITH UpsertSrc AS
        (
            SELECT TabID, FK_LocationID, CreatedBy, FK_AccountID, FK_CostCenterID, FK_PaymentTypeID, TabName, TableName, NoOfGuests, Gratuity, GratuityPerc, Discount, DiscountPerc, IsVoided, VoidNote, IsPaid, AmountPaid, AmountDue, VatTotal, PaymentDate, ClosedDate, AdditionalInfo, DateCreated, DateUpdated, FK_CurrencyID, CurrentExchangeRate
            FROM #Src
            WHERE ISNULL(SyncStatus, 'NOT_SYNCED') <> 'DELETE_PENDING'
        )
        MERGE dbo.POS_Tabs AS T
        USING UpsertSrc AS S
          ON T.TabID = S.TabID
        WHEN MATCHED THEN
            UPDATE SET
                T.FK_LocationID = S.FK_LocationID,
                T.CreatedBy = S.CreatedBy,
                T.FK_AccountID = S.FK_AccountID,
                T.FK_CostCenterID = S.FK_CostCenterID,
                T.FK_PaymentTypeID = S.FK_PaymentTypeID,
                T.TabName = S.TabName,
                T.TableName = S.TableName,
                T.NoOfGuests = S.NoOfGuests,
                T.Gratuity = S.Gratuity,
                T.GratuityPerc = S.GratuityPerc,
                T.Discount = S.Discount,
                T.DiscountPerc = S.DiscountPerc,
                T.IsVoided = S.IsVoided,
                T.VoidNote = S.VoidNote,
                T.IsPaid = S.IsPaid,
                T.AmountPaid = S.AmountPaid,
                T.AmountDue = S.AmountDue,
                T.VatTotal = S.VatTotal,
                T.PaymentDate = S.PaymentDate,
                T.ClosedDate = S.ClosedDate,
                T.AdditionalInfo = S.AdditionalInfo,
                T.DateCreated = S.DateCreated,
                T.DateUpdated = S.DateUpdated,
                T.FK_CurrencyID = S.FK_CurrencyID,
                T.CurrentExchangeRate = S.CurrentExchangeRate
        WHEN NOT MATCHED BY TARGET THEN
            INSERT (TabID, FK_LocationID, CreatedBy, FK_AccountID, FK_CostCenterID, FK_PaymentTypeID, TabName, TableName, NoOfGuests, Gratuity, GratuityPerc, Discount, DiscountPerc, IsVoided, VoidNote, IsPaid, AmountPaid, AmountDue, VatTotal, PaymentDate, ClosedDate, AdditionalInfo, DateCreated, DateUpdated, FK_CurrencyID, CurrentExchangeRate)
            VALUES (S.TabID, S.FK_LocationID, S.CreatedBy, S.FK_AccountID, S.FK_CostCenterID, S.FK_PaymentTypeID, S.TabName, S.TableName, S.NoOfGuests, S.Gratuity, S.GratuityPerc, S.Discount, S.DiscountPerc, S.IsVoided, S.VoidNote, S.IsPaid, S.AmountPaid, S.AmountDue, S.VatTotal, S.PaymentDate, S.ClosedDate, S.AdditionalInfo, S.DateCreated, S.DateUpdated, S.FK_CurrencyID, S.CurrentExchangeRate);

        COMMIT TRAN;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRAN;
        THROW;
    END CATCH
END
GO