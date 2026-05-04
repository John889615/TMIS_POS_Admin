USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.debtorMenuTree_select_all', 'P') IS NOT NULL
    DROP PROCEDURE dbo.debtorMenuTree_select_all;
GO

-- exec debtorMenuTree_select_all 1

CREATE PROCEDURE dbo.debtorMenuTree_select_all

	@DebtorMenuID INT
AS
BEGIN
    SELECT
        dm.DebtorMenuID AS MenuID,
        dm.MenuName,
		dmi.DebtorMenuItemID AS ItemID,
		dmi.Item,
		dpmi.DebtorMenuItemID AS ParentItemID,
		dpmi.Item AS ParentItem,
		dmip.MenuItemProductID,
		dmip.FK_ProductID AS ProductID,
		dmip.DisplayOrder,
		p.ProductName AS [Product],
        dm.DateCreated,
        dm.DateUpdated,
        dm.ValidFrom,
        dm.ValidTo
    FROM POS_DebtorMenus dm
	LEFT JOIN POS_DebtorMenuItems dmi
	ON (dm.DebtorMenuID = dmi.FK_DebtorMenuID)
	LEFT JOIN POS_DebtorMenuItemProducts dmip
	ON (dmi.DebtorMenuItemID = dmip.FK_DebtorMenuItemID)
    LEFT JOIN POS_DebtorMenuItems dpmi
    ON dpmi.DebtorMenuItemID = dmi.FK_MenuItemID
	LEFT JOIN POS_Products p
	ON (dmip.FK_ProductID = p.ProductID)
    WHERE dm.DebtorMenuID = @DebtorMenuID

	ORDER BY dm.DebtorMenuID, dmi.DebtorMenuItemID, dmip.DisplayOrder, dmip.MenuItemProductID;
END
GO