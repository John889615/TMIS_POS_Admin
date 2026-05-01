-- =============================================================
-- Migration : Stock Request Line - Unit of Measure
-- Date      : 2026-05-01
-- Step      : 01 of 02 (Schema only. Run BEFORE the code generator.)
--
-- After running this script:
--   1. Drop the regenerable line CRUD SPs so the generator rebuilds them:
--        DROP PROCEDURE dbo.POS_StockRequestLines_insert;
--        DROP PROCEDURE dbo.POS_StockRequestLines_update;
--      (POS_StockRequestLines_select_single / _select_all use SELECT *
--       and pick the new column up automatically - no drop needed.)
--   2. Run the code generator. It should regenerate:
--        - StockRequestLine_Base.cs            (new FK_UnitID property)
--        - Stock_Base_Service.cs CRUD wrappers (new @FK_UnitID param)
--        - Stock_Base_Translator.cs            (new FK_UnitID column)
--        - POS_StockRequestLines_insert / _update SPs
--   3. Then run Script 02 (custom SP that joins POS_Units).
--
-- This script is idempotent - safe to re-run.
-- =============================================================
USE [TMIS_Development];
GO

-- ----- POS_StockRequestLines : unit of measure --------------
IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[POS_StockRequestLines]') AND name = N'FK_UnitID')
BEGIN
    ALTER TABLE [dbo].[POS_StockRequestLines]
        ADD [FK_UnitID] INT NULL;
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = N'FK_POS_StockRequestLines_Unit')
BEGIN
    ALTER TABLE [dbo].[POS_StockRequestLines] WITH CHECK
        ADD CONSTRAINT [FK_POS_StockRequestLines_Unit]
        FOREIGN KEY ([FK_UnitID]) REFERENCES [dbo].[POS_Units] ([UnitID]);
END
GO
