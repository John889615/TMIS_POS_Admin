USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.menuTree_select', 'P') IS NOT NULL
    DROP PROCEDURE dbo.menuTree_select;
GO

CREATE PROCEDURE dbo.menuTree_select
	@MenuID INT
AS
BEGIN
    SELECT
    m.MenuID,
    m.MenuName,
	mi.MenuItemID AS ItemID,
    mi.Item AS Item,
	pmi.MenuItemID AS ParentItemID,
	pmi.Item AS ParentItem,
	mip.MenuItemProductID,
    mip.FK_ProductID AS ProductID,
	mip.DisplayOrder,
	p.Description As [Product]
FROM POS_Menus m
LEFT JOIN POS_MenuItems mi
ON m.MenuID = mi.FK_MenuID
LEFT JOIN POS_MenuItemProducts mip
ON mi.MenuItemID = mip.FK_MenuItemID
LEFT JOIN POS_MenuItems pmi
ON pmi.MenuItemID = mi.FK_MenuItemID
LEFT JOIN POS_Products p
ON p.ProductID = mip.FK_ProductID
WHERE m.MenuID = @MenuID
ORDER BY m.MenuID, mi.MenuItemID, mip.DisplayOrder, mip.MenuItemProductID;
END
GO