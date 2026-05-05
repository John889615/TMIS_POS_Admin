-- =============================================================
-- Migration : Menu Product - Display Order (drag-and-drop ordering)
-- Date      : 2026-05-02
-- Step      : 01 of 02 (Schema only. Run BEFORE the code generator.)
--
-- Adds a DisplayOrder column to the two menu-item-product tables so
-- drag-and-drop reorder in the menu builders can be persisted, and
-- the menu trees come back in user-defined order.
--
-- After running this script:
--   1. Drop the regenerable CRUD SPs so the generator rebuilds them:
--        DROP PROCEDURE dbo.POS_MenuItemProducts_insert;
--        DROP PROCEDURE dbo.POS_MenuItemProducts_update;
--        DROP PROCEDURE dbo.POS_DebtorMenuItemProducts_insert;
--        DROP PROCEDURE dbo.POS_DebtorMenuItemProducts_update;
--      (POS_*_select_single / _select_all use SELECT * and pick the
--       new column up automatically - no drop needed.)
--   2. Run the code generator. It should regenerate:
--        - MenuItemProduct_Base.cs / DebtorMenuItemProduct_Base.cs (new DisplayOrder property)
--        - Menu_Base_Service.cs CRUD wrappers (new @DisplayOrder param)
--        - Menu_Base_Translator.cs (new DisplayOrder column)
--        - POS_MenuItemProducts_insert / _update SPs
--        - POS_DebtorMenuItemProducts_insert / _update SPs
--   3. Then run Script 02 (custom reorder SPs + tree-select ORDER BY changes).
--
-- This script is idempotent - safe to re-run.
--
-- Note: the backfill UPDATE is wrapped in sp_executesql so its compilation
-- is deferred until runtime - SQL Server compiles a whole batch up-front
-- and would otherwise reject the UPDATE because the column the ALTER is
-- about to add does not exist yet.
-- =============================================================
USE [TMIS_Development];
GO

-- ----- POS_MenuItemProducts : display order -----------------
IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[POS_MenuItemProducts]') AND name = N'DisplayOrder')
BEGIN
    ALTER TABLE [dbo].[POS_MenuItemProducts]
        ADD [DisplayOrder] INT NOT NULL CONSTRAINT [DF_POS_MenuItemProducts_DisplayOrder] DEFAULT (0);

    -- Backfill: assign sequential DisplayOrder per menu item, ordered by current ID.
    -- Only fires on the initial column add (since the IF NOT EXISTS guarded it).
    EXEC sp_executesql N'
        WITH ranked AS
        (
            SELECT
                MenuItemProductID,
                ROW_NUMBER() OVER (PARTITION BY FK_MenuItemID ORDER BY MenuItemProductID) - 1 AS NewOrder
            FROM [dbo].[POS_MenuItemProducts]
        )
        UPDATE p
           SET p.DisplayOrder = r.NewOrder
          FROM [dbo].[POS_MenuItemProducts] p
          JOIN ranked r ON r.MenuItemProductID = p.MenuItemProductID;
    ';
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'IX_POS_MenuItemProducts_MenuItem_DisplayOrder' AND object_id = OBJECT_ID(N'[dbo].[POS_MenuItemProducts]'))
BEGIN
    CREATE INDEX [IX_POS_MenuItemProducts_MenuItem_DisplayOrder]
        ON [dbo].[POS_MenuItemProducts] ([FK_MenuItemID], [DisplayOrder]);
END
GO

-- ----- POS_DebtorMenuItemProducts : display order -----------
IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[POS_DebtorMenuItemProducts]') AND name = N'DisplayOrder')
BEGIN
    ALTER TABLE [dbo].[POS_DebtorMenuItemProducts]
        ADD [DisplayOrder] INT NOT NULL CONSTRAINT [DF_POS_DebtorMenuItemProducts_DisplayOrder] DEFAULT (0);

    -- Backfill: same approach, partitioned by debtor menu item.
    EXEC sp_executesql N'
        WITH ranked AS
        (
            SELECT
                MenuItemProductID,
                ROW_NUMBER() OVER (PARTITION BY FK_DebtorMenuItemID ORDER BY MenuItemProductID) - 1 AS NewOrder
            FROM [dbo].[POS_DebtorMenuItemProducts]
        )
        UPDATE p
           SET p.DisplayOrder = r.NewOrder
          FROM [dbo].[POS_DebtorMenuItemProducts] p
          JOIN ranked r ON r.MenuItemProductID = p.MenuItemProductID;
    ';
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'IX_POS_DebtorMenuItemProducts_DebtorMenuItem_DisplayOrder' AND object_id = OBJECT_ID(N'[dbo].[POS_DebtorMenuItemProducts]'))
BEGIN
    CREATE INDEX [IX_POS_DebtorMenuItemProducts_DebtorMenuItem_DisplayOrder]
        ON [dbo].[POS_DebtorMenuItemProducts] ([FK_DebtorMenuItemID], [DisplayOrder]);
END
GO
