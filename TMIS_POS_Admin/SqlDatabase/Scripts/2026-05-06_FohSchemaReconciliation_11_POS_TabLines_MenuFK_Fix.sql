-- =============================================================
-- Migration : FOH-to-Admin Schema Reconciliation - addendum
-- Date      : 2026-05-06
-- Step      : 11 of 11 - POS_TabLines.FK_MenuID -> POS_DebtorMenus
--
-- Spec 1 script 04 originally created the FK_MenuID foreign key
-- pointing at POS_Menus(MenuID). On the Admin side menus are split
-- into POS_Menus (master) and POS_DebtorMenus (per-debtor instances);
-- the FOH MenuID corresponds to a DebtorMenuID. This script repoints
-- the FK to dbo.POS_DebtorMenus(DebtorMenuID).
--
-- Idempotent. Safe to re-run.
-- =============================================================
USE [TMIS_Development];
GO

-- ----- Drop the old FK if it exists -------------------------
IF EXISTS (SELECT 1 FROM sys.foreign_keys
           WHERE parent_object_id = OBJECT_ID(N'[dbo].[POS_TabLines]')
             AND name = N'FK_POS_TabLines_Menu')
BEGIN
    ALTER TABLE [dbo].[POS_TabLines]
        DROP CONSTRAINT [FK_POS_TabLines_Menu];
END
GO

-- ----- Drop any auto-named FK on FK_MenuID (defensive) ------
DECLARE @autoFk SYSNAME;
SELECT TOP 1 @autoFk = fk.name
  FROM sys.foreign_keys fk
  JOIN sys.foreign_key_columns fkc
    ON fkc.constraint_object_id = fk.object_id
  JOIN sys.columns c
    ON c.object_id = fkc.parent_object_id
   AND c.column_id = fkc.parent_column_id
 WHERE fk.parent_object_id = OBJECT_ID(N'[dbo].[POS_TabLines]')
   AND c.name = N'FK_MenuID';

IF @autoFk IS NOT NULL
    EXEC ('ALTER TABLE [dbo].[POS_TabLines] DROP CONSTRAINT [' + @autoFk + ']');
GO

-- ----- Create the new FK pointing at POS_DebtorMenus --------
IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys
               WHERE parent_object_id = OBJECT_ID(N'[dbo].[POS_TabLines]')
                 AND name = N'FK_POS_TabLines_DebtorMenu')
BEGIN
    ALTER TABLE [dbo].[POS_TabLines]
        ADD CONSTRAINT [FK_POS_TabLines_DebtorMenu]
            FOREIGN KEY ([FK_MenuID])
            REFERENCES [dbo].[POS_DebtorMenus] ([DebtorMenuID]);
END
GO
