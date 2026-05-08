USE [TMIS_Development]
GO

IF OBJECT_ID('dbo.BulkUpsertToServer_TabLines', 'P') IS NOT NULL
    DROP PROCEDURE dbo.BulkUpsertToServer_TabLines;
GO

-- =============================================================
-- Updated 2026-05-06 for FOH schema reconciliation (Spec 1):
--   - Adds Gratuity, GratuityPerc.
--   - Switches DiscountPerc from INT slot to DECIMAL slot
--     (FOH widened DiscountPerc INT -> DECIMAL(18,4)).
--
-- ServedAs / ServedAsQuantified / ServedAsQuantity / FK_MenuID /
-- MenuName were already in place; preserved.
--
-- Slot mapping:
--   Guid1   TabLineID              Bool1   IsVoided
--   Guid2   FK_TabID               Bool2   ServedAsQuantified
--   Guid3   FK_PointerID           Date1   DateCreated
--   Int1    FK_CostCenterID        Date2   DateUpdated
--   Int2    FK_ProductID           Decimal1 UnitCostExcl
--   Int3    FK_PriceCodeID         Decimal2 Vat
--   Int5    FK_MenuID              Decimal3 UnitCostIncl
--   String1 Product (50)           Decimal4 Quantity
--   String2 Notes (MAX)            Decimal5 Discount
--   String3 AutoNotes (MAX)        Decimal6 ServedAsQuantity
--   String4 ServedAs (50)          Decimal7 DiscountPerc
--   String5 MenuName (100)         Decimal8 Gratuity
--   String6 CreatedBy (255)        Decimal9 GratuityPerc
--   String7 SyncStatus (50)
-- =============================================================
CREATE PROCEDURE dbo.BulkUpsertToServer_TabLines
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
                Guid1 AS TabLineID,
                Guid2 AS FK_TabID,
                CAST(String6 AS VARCHAR(255)) AS CreatedBy,
                Int2  AS FK_ProductID,
                Int3  AS FK_PriceCodeID,
                Int1  AS FK_CostCenterID,
                Guid3 AS FK_PointerID,
                Decimal1 AS UnitCostExcl,
                Decimal2 AS Vat,
                Decimal3 AS UnitCostIncl,
                CAST(String1 AS VARCHAR(50))  AS Product,
                Decimal4 AS Quantity,
                Decimal5 AS Discount,
                Decimal7 AS DiscountPerc,
                Bool1   AS IsVoided,
                CAST(String2 AS VARCHAR(MAX)) AS Notes,
                CAST(String3 AS VARCHAR(MAX)) AS AutoNotes,
                Date1   AS DateCreated,
                Date2   AS DateUpdated,
                CAST(String4 AS VARCHAR(50))  AS ServedAs,
                Bool2   AS ServedAsQuantified,
                Decimal6 AS ServedAsQuantity,
                Int5    AS FK_MenuID,
                CAST(String5 AS VARCHAR(100)) AS MenuName,
                Decimal8 AS Gratuity,
                Decimal9 AS GratuityPerc,
                CAST(String7 AS VARCHAR(50))  AS SyncStatus
            FROM @Rows
            WHERE Guid1 IS NOT NULL
        )
        SELECT * INTO #Src FROM Src;

        IF EXISTS (SELECT 1 FROM #Src WHERE SyncStatus = 'DELETE_PENDING')
        BEGIN
            SELECT DISTINCT TabLineID
            INTO #DeleteTabLines
            FROM #Src
            WHERE SyncStatus = 'DELETE_PENDING';

            SELECT DISTINCT tlc.TabLineCombinationID
            INTO #DeleteTabLineCombinations
            FROM dbo.POS_TabLineCombinations tlc
            INNER JOIN #DeleteTabLines tl
                ON tl.TabLineID = tlc.FK_TabLineID;

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
            INNER JOIN #DeleteTabLines x
                ON x.TabLineID = vl.FK_TabLineID;

            DELETE tlc
            FROM dbo.POS_TabLineCombinations tlc
            INNER JOIN #DeleteTabLineCombinations x
                ON x.TabLineCombinationID = tlc.TabLineCombinationID;

            DELETE tl
            FROM dbo.POS_TabLines tl
            INNER JOIN #DeleteTabLines x
                ON x.TabLineID = tl.TabLineID;
        END

        ;WITH UpsertSrc AS
        (
            SELECT TabLineID, FK_TabID, CreatedBy, FK_ProductID, FK_PriceCodeID,
                   FK_CostCenterID,
                   FK_PointerID, UnitCostExcl, Vat, UnitCostIncl, Product,
                   Quantity, Discount, DiscountPerc, IsVoided, Notes, AutoNotes,
                   DateCreated, DateUpdated, ServedAs, ServedAsQuantified,
                   ServedAsQuantity, FK_MenuID, MenuName, Gratuity, GratuityPerc
            FROM #Src
            WHERE ISNULL(SyncStatus, 'NOT_SYNCED') <> 'DELETE_PENDING'
        )
        MERGE dbo.POS_TabLines AS T
        USING UpsertSrc AS S
          ON T.TabLineID = S.TabLineID
        WHEN MATCHED THEN
            UPDATE SET
                T.FK_TabID            = S.FK_TabID,
                T.CreatedBy           = S.CreatedBy,
                T.FK_ProductID        = S.FK_ProductID,
                T.FK_PriceCodeID      = S.FK_PriceCodeID,
                T.FK_CostCenterID     = S.FK_CostCenterID,
                T.FK_PointerID        = S.FK_PointerID,
                T.UnitCostExcl        = S.UnitCostExcl,
                T.Vat                 = S.Vat,
                T.UnitCostIncl        = S.UnitCostIncl,
                T.Product             = S.Product,
                T.Quantity            = S.Quantity,
                T.Discount            = S.Discount,
                T.DiscountPerc        = S.DiscountPerc,
                T.IsVoided            = S.IsVoided,
                T.Notes               = S.Notes,
                T.AutoNotes           = S.AutoNotes,
                T.DateCreated         = S.DateCreated,
                T.DateUpdated         = S.DateUpdated,
                T.ServedAs            = S.ServedAs,
                T.ServedAsQuantified  = S.ServedAsQuantified,
                T.ServedAsQuantity    = S.ServedAsQuantity,
                T.FK_MenuID           = S.FK_MenuID,
                T.MenuName            = S.MenuName,
                T.Gratuity            = S.Gratuity,
                T.GratuityPerc        = S.GratuityPerc
        WHEN NOT MATCHED BY TARGET THEN
            INSERT (TabLineID, FK_TabID, CreatedBy, FK_ProductID, FK_PriceCodeID,
                    FK_CostCenterID,
                    FK_PointerID, UnitCostExcl, Vat, UnitCostIncl, Product,
                    Quantity, Discount, DiscountPerc, IsVoided, Notes, AutoNotes,
                    DateCreated, DateUpdated, ServedAs, ServedAsQuantified,
                    ServedAsQuantity, FK_MenuID, MenuName, Gratuity, GratuityPerc)
            VALUES (S.TabLineID, S.FK_TabID, S.CreatedBy, S.FK_ProductID, S.FK_PriceCodeID,
                    S.FK_CostCenterID,
                    S.FK_PointerID, S.UnitCostExcl, S.Vat, S.UnitCostIncl, S.Product,
                    S.Quantity, S.Discount, S.DiscountPerc, S.IsVoided, S.Notes, S.AutoNotes,
                    S.DateCreated, S.DateUpdated, S.ServedAs, S.ServedAsQuantified,
                    S.ServedAsQuantity, S.FK_MenuID, S.MenuName, S.Gratuity, S.GratuityPerc);

        COMMIT TRAN;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRAN;
        THROW;
    END CATCH
END
GO
