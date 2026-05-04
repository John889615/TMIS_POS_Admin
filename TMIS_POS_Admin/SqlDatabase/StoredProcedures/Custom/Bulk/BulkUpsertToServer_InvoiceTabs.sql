USE [TMIS_Development]
GO

IF OBJECT_ID('dbo.BulkUpsertToServer_InvoiceTabs', 'P') IS NOT NULL
    DROP PROCEDURE dbo.BulkUpsertToServer_InvoiceTabs;
GO

CREATE PROCEDURE dbo.BulkUpsertToServer_InvoiceTabs
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
                Guid1 AS InvoiceTabID,
                Guid2 AS FK_InvoiceHeaderID,
                Guid3 AS FK_TabID,
                Decimal1 AS TabGratuity,
                Decimal2 AS TabDiscount,
                Decimal3 AS TabTotalExcl,
                Decimal4 AS TabTotalVat,
                Decimal5 AS TabTotalIncl,
                Date1 AS TabDateOpened,
                Date2 AS TabDateClosed,
                CAST(String1 AS VARCHAR(50)) AS SyncStatus
            FROM @Rows
            WHERE Guid1 IS NOT NULL
        )
        SELECT * INTO #Src FROM Src;

        IF EXISTS (SELECT 1 FROM #Src WHERE SyncStatus = 'DELETE_PENDING')
        BEGIN
            SELECT DISTINCT InvoiceTabID
            INTO #DeleteTabs
            FROM #Src
            WHERE SyncStatus = 'DELETE_PENDING';

            DELETE il
            FROM dbo.POS_InvoiceLines il
            INNER JOIN #DeleteTabs x
                ON x.InvoiceTabID = il.FK_InvoiceTabID;

            DELETE it
            FROM dbo.POS_InvoiceTabs it
            INNER JOIN #DeleteTabs x
                ON x.InvoiceTabID = it.InvoiceTabID;
        END

        ;WITH UpsertSrc AS
        (
            SELECT InvoiceTabID, FK_InvoiceHeaderID, FK_TabID, TabGratuity, TabDiscount, TabTotalExcl, TabTotalVat, TabTotalIncl, TabDateOpened, TabDateClosed
            FROM #Src
            WHERE ISNULL(SyncStatus, 'NOT_SYNCED') <> 'DELETE_PENDING'
        )
        MERGE dbo.POS_InvoiceTabs AS T
        USING UpsertSrc AS S
          ON T.InvoiceTabID = S.InvoiceTabID
        WHEN MATCHED THEN
            UPDATE SET
                T.FK_InvoiceHeaderID = S.FK_InvoiceHeaderID,
                T.FK_TabID = S.FK_TabID,
                T.TabGratuity = S.TabGratuity,
                T.TabDiscount = S.TabDiscount,
                T.TabTotalExcl = S.TabTotalExcl,
                T.TabTotalVat = S.TabTotalVat,
                T.TabTotalIncl = S.TabTotalIncl,
                T.TabDateOpened = S.TabDateOpened,
                T.TabDateClosed = S.TabDateClosed
        WHEN NOT MATCHED BY TARGET THEN
            INSERT (InvoiceTabID, FK_InvoiceHeaderID, FK_TabID, TabGratuity, TabDiscount, TabTotalExcl, TabTotalVat, TabTotalIncl, TabDateOpened, TabDateClosed, SyncedToServer)
            VALUES (S.InvoiceTabID, S.FK_InvoiceHeaderID, S.FK_TabID, S.TabGratuity, S.TabDiscount, S.TabTotalExcl, S.TabTotalVat, S.TabTotalIncl, S.TabDateOpened, S.TabDateClosed, 0);

        COMMIT TRAN;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRAN;
        THROW;
    END CATCH
END
GO