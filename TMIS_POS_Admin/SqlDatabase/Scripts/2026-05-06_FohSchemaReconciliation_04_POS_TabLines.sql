-- =============================================================
-- Migration : FOH-to-Admin Schema Reconciliation
-- Date      : 2026-05-06
-- Step      : 04 of 07 - POS_TabLines
--
-- Aligns POS_TabLines with FOH TabLines:
--   - DiscountPerc INT NULL --> DECIMAL(18,4) NULL  (widen)
--   - ADD ServedAs           VARCHAR(50)    NULL
--   - ADD ServedAsQuantified BIT            NULL
--   - ADD ServedAsQuantity   DECIMAL(18,4)  NULL
--   - ADD FK_MenuID          INT            NULL FK -> POS_Menus(MenuID)
--   - ADD MenuName           VARCHAR(100)   NULL
--   - ADD Gratuity           DECIMAL(18,4)  NULL
--   - ADD GratuityPerc       DECIMAL(18,4)  NULL
--
-- Note: Admin keeps the existing CreatedBy VARCHAR(255) NN per the
-- staff-not-synced rule; FOH's FK_StaffID is NOT mirrored as a column
-- on Admin. The sync layer (Spec 2) populates CreatedBy from
-- Staff.Name + ' ' + Staff.Surname on the FOH side.
--
-- After running this script:
--   1. Drop regenerable CRUD SPs so the generator rebuilds them:
--        DROP PROCEDURE dbo.POS_TabLines_insert;
--        DROP PROCEDURE dbo.POS_TabLines_update;
--   2. Run the code generator.
--   3. Then move on to script 05.
--
-- This script is idempotent - safe to re-run.
-- =============================================================
USE [TMIS_Development];
GO

-- ----- DiscountPerc INT --> DECIMAL(18,4) --------------------
IF EXISTS (SELECT 1 FROM sys.columns c
           JOIN sys.types t ON t.user_type_id = c.user_type_id
           WHERE c.object_id = OBJECT_ID(N'[dbo].[POS_TabLines]')
             AND c.name = N'DiscountPerc'
             AND t.name = N'int')
BEGIN
    ALTER TABLE [dbo].[POS_TabLines] ALTER COLUMN [DiscountPerc] DECIMAL(18,4) NULL;
END
GO

-- ----- ADD ServedAs ------------------------------------------
IF NOT EXISTS (SELECT 1 FROM sys.columns
               WHERE object_id = OBJECT_ID(N'[dbo].[POS_TabLines]')
                 AND name = N'ServedAs')
BEGIN
    ALTER TABLE [dbo].[POS_TabLines] ADD [ServedAs] VARCHAR(50) NULL;
END
GO

-- ----- ADD ServedAsQuantified --------------------------------
IF NOT EXISTS (SELECT 1 FROM sys.columns
               WHERE object_id = OBJECT_ID(N'[dbo].[POS_TabLines]')
                 AND name = N'ServedAsQuantified')
BEGIN
    ALTER TABLE [dbo].[POS_TabLines] ADD [ServedAsQuantified] BIT NULL;
END
GO

-- ----- ADD ServedAsQuantity ----------------------------------
IF NOT EXISTS (SELECT 1 FROM sys.columns
               WHERE object_id = OBJECT_ID(N'[dbo].[POS_TabLines]')
                 AND name = N'ServedAsQuantity')
BEGIN
    ALTER TABLE [dbo].[POS_TabLines] ADD [ServedAsQuantity] DECIMAL(18,4) NULL;
END
GO

-- ----- ADD FK_MenuID + FK constraint -------------------------
IF NOT EXISTS (SELECT 1 FROM sys.columns
               WHERE object_id = OBJECT_ID(N'[dbo].[POS_TabLines]')
                 AND name = N'FK_MenuID')
BEGIN
    ALTER TABLE [dbo].[POS_TabLines] ADD [FK_MenuID] INT NULL;
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys
               WHERE parent_object_id = OBJECT_ID(N'[dbo].[POS_TabLines]')
                 AND name = N'FK_POS_TabLines_Menu')
BEGIN
    ALTER TABLE [dbo].[POS_TabLines]
        ADD CONSTRAINT [FK_POS_TabLines_Menu]
            FOREIGN KEY ([FK_MenuID]) REFERENCES [dbo].[POS_Menus] ([MenuID]);
END
GO

-- ----- ADD MenuName ------------------------------------------
IF NOT EXISTS (SELECT 1 FROM sys.columns
               WHERE object_id = OBJECT_ID(N'[dbo].[POS_TabLines]')
                 AND name = N'MenuName')
BEGIN
    ALTER TABLE [dbo].[POS_TabLines] ADD [MenuName] VARCHAR(100) NULL;
END
GO

-- ----- ADD Gratuity ------------------------------------------
IF NOT EXISTS (SELECT 1 FROM sys.columns
               WHERE object_id = OBJECT_ID(N'[dbo].[POS_TabLines]')
                 AND name = N'Gratuity')
BEGIN
    ALTER TABLE [dbo].[POS_TabLines] ADD [Gratuity] DECIMAL(18,4) NULL;
END
GO

-- ----- ADD GratuityPerc --------------------------------------
IF NOT EXISTS (SELECT 1 FROM sys.columns
               WHERE object_id = OBJECT_ID(N'[dbo].[POS_TabLines]')
                 AND name = N'GratuityPerc')
BEGIN
    ALTER TABLE [dbo].[POS_TabLines] ADD [GratuityPerc] DECIMAL(18,4) NULL;
END
GO
