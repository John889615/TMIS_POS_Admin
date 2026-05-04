USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.debtorMenuItemProducts_reorder', 'P') IS NOT NULL
    DROP PROCEDURE dbo.debtorMenuItemProducts_reorder;
GO

-- exec debtorMenuItemProducts_reorder 1, '[10, 7, 12, 3]'
--
-- Updates DisplayOrder on all POS_DebtorMenuItemProducts rows for a given
-- debtor menu item. The order is taken from the position of each
-- MenuItemProductID in the JSON array argument (0-based). Rows in the
-- debtor menu item that are not mentioned in the array are left
-- untouched - the caller is expected to pass the full list. The WHERE
-- on @FK_DebtorMenuItemID ensures rows belonging to other menu items
-- can never be touched by this call.

CREATE PROCEDURE dbo.debtorMenuItemProducts_reorder
    @FK_DebtorMenuItemID INT,
    @OrderedIDs          NVARCHAR(MAX)
AS
BEGIN
    -- NOTE: do NOT add SET NOCOUNT ON here. ExecuteNonQueryAsync uses the
    -- DONE_IN_PROC row-count messages to populate its return value; with
    -- NOCOUNT ON it returns -1 even though the UPDATE actually ran.

    ;WITH ordered AS
    (
        SELECT
            CAST([key]   AS INT) AS DisplayOrder,
            CAST([value] AS INT) AS MenuItemProductID
        FROM OPENJSON(@OrderedIDs)
    )
    UPDATE p
       SET p.DisplayOrder = o.DisplayOrder
      FROM POS_DebtorMenuItemProducts p
      JOIN ordered o ON o.MenuItemProductID = p.MenuItemProductID
     WHERE p.FK_DebtorMenuItemID = @FK_DebtorMenuItemID;
END
GO
