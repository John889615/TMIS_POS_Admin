USE [TMIS_Development]
GO

IF OBJECT_ID('dbo.BulkUpsertToServer_InvoiceLines', 'P') IS NOT NULL
    DROP PROCEDURE dbo.BulkUpsertToServer_InvoiceLines;
GO

CREATE PROCEDURE dbo.BulkUpsertToServer_InvoiceLines
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
                Guid1 AS InvoiceLineID,
                Guid2 AS FK_InvoiceTabID,
                CAST(String1 AS VARCHAR(100)) AS Product,
                Decimal1 AS Quantity,
                Decimal2 AS LineDiscount,
                Decimal3 AS LineTotalExcl,
                Decimal4 AS LineTotalVat,
                Decimal5 AS LineTotalIncl,
                CAST(String2 AS VARCHAR(100)) AS Guests,
                Int1 AS FK_ProductID,
                CAST(String3 AS VARCHAR(50)) AS SyncStatus
            FROM @Rows
            WHERE Guid1 IS NOT NULL
        )
        SELECT * INTO #Src FROM Src;

        DELETE il
        FROM dbo.POS_InvoiceLines il
        INNER JOIN #Src s
            ON s.InvoiceLineID = il.InvoiceLineID
        WHERE s.SyncStatus = 'DELETE_PENDING';

        ;WITH UpsertSrc AS
        (
            SELECT InvoiceLineID, FK_InvoiceTabID, Product, Quantity, LineDiscount, LineTotalExcl, LineTotalVat, LineTotalIncl, Guests, FK_ProductID
            FROM #Src
            WHERE ISNULL(SyncStatus, 'NOT_SYNCED') <> 'DELETE_PENDING'
        )
        MERGE dbo.POS_InvoiceLines AS T
        USING UpsertSrc AS S
          ON T.InvoiceLineID = S.InvoiceLineID
        WHEN MATCHED THEN
            UPDATE SET
                T.FK_InvoiceTabID = S.FK_InvoiceTabID,
                T.Product = S.Product,
                T.Quantity = S.Quantity,
                T.LineDiscount = S.LineDiscount,
                T.LineTotalExcl = S.LineTotalExcl,
                T.LineTotalVat = S.LineTotalVat,
                T.LineTotalIncl = S.LineTotalIncl,
                T.Guests = S.Guests,
                T.FK_ProductID = S.FK_ProductID
        WHEN NOT MATCHED BY TARGET THEN
            INSERT (InvoiceLineID, FK_InvoiceTabID, Product, Quantity, LineDiscount, LineTotalExcl, LineTotalVat, LineTotalIncl, Guests, SyncedToServer, FK_ProductID)
            VALUES (S.InvoiceLineID, S.FK_InvoiceTabID, S.Product, S.Quantity, S.LineDiscount, S.LineTotalExcl, S.LineTotalVat, S.LineTotalIncl, S.Guests, 0, S.FK_ProductID);

        COMMIT TRAN;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRAN;
        THROW;
    END CATCH
END
GO