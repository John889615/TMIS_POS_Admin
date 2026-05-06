-- =============================================================
-- Migration : FOH-to-Admin Schema Reconciliation
-- Date      : 2026-05-06
-- Step      : 03 of 07 - POS_Tabs
--
-- Aligns POS_Tabs with FOH Tabs:
--   - DiscountPerc INT NULL --> DECIMAL(18,4) NULL  (widen)
--   - GratuityPerc INT NULL --> DECIMAL(18,4) NULL  (widen)
--   - ADD TableNumber INT NULL                       (FOH has both TableName INT
--                                                     AND TableNumber INT - keeping
--                                                     parity even though it looks
--                                                     redundant)
--
-- After running this script:
--   1. Drop regenerable CRUD SPs so the generator rebuilds them:
--        DROP PROCEDURE dbo.POS_Tabs_insert;
--        DROP PROCEDURE dbo.POS_Tabs_update;
--   2. Run the code generator. It should regenerate:
--        - Tab_Base.cs (DiscountPerc/GratuityPerc now decimal, new TableNumber)
--        - Sync_Base_Service.cs CRUD wrappers
--        - Sync_Base_Translator.cs
--        - POS_Tabs_insert / _update SPs
--   3. Then move on to script 04.
--
-- This script is idempotent - safe to re-run.
-- =============================================================
USE [TMIS_Development];
GO

-- ----- DiscountPerc INT --> DECIMAL(18,4) --------------------
IF EXISTS (SELECT 1 FROM sys.columns c
           JOIN sys.types t ON t.user_type_id = c.user_type_id
           WHERE c.object_id = OBJECT_ID(N'[dbo].[POS_Tabs]')
             AND c.name = N'DiscountPerc'
             AND t.name = N'int')
BEGIN
    ALTER TABLE [dbo].[POS_Tabs] ALTER COLUMN [DiscountPerc] DECIMAL(18,4) NULL;
END
GO

-- ----- GratuityPerc INT --> DECIMAL(18,4) --------------------
IF EXISTS (SELECT 1 FROM sys.columns c
           JOIN sys.types t ON t.user_type_id = c.user_type_id
           WHERE c.object_id = OBJECT_ID(N'[dbo].[POS_Tabs]')
             AND c.name = N'GratuityPerc'
             AND t.name = N'int')
BEGIN
    ALTER TABLE [dbo].[POS_Tabs] ALTER COLUMN [GratuityPerc] DECIMAL(18,4) NULL;
END
GO

-- ----- ADD TableNumber INT NULL ------------------------------
IF NOT EXISTS (SELECT 1 FROM sys.columns
               WHERE object_id = OBJECT_ID(N'[dbo].[POS_Tabs]')
                 AND name = N'TableNumber')
BEGIN
    ALTER TABLE [dbo].[POS_Tabs] ADD [TableNumber] INT NULL;
END
GO
