USE [TMIS_BlueSafaris]
GO


IF OBJECT_ID('dbo.menuTree_select_all', 'P') IS NOT NULL
    DROP PROCEDURE dbo.menuTree_select_all;
GO

CREATE PROCEDURE dbo.menuTree_select_all
	
AS
BEGIN
    SELECT 
    m.POS_MenuID,
    m.MenuName,
	mi.POS_MenuItemID AS ItemID,
    mi.Item AS Item,
	pmi.POS_MenuItemID AS ParentItemID,
	pmi.Item AS ParentItem,
	mip.POS_MenuItemProductID,
    mip.FK_ProductID AS ProductID,
	p.Description As [Product]
FROM POS_Menus m
LEFT JOIN POS_MenuItems mi
ON m.POS_MenuID = mi.FK_MenuID
LEFT JOIN POS_MenuItemProducts mip
ON mi.POS_MenuItemID = mip.FK_MenuItemID
LEFT JOIN POS_MenuItems pmi 
ON pmi.POS_MenuItemID = mi.FK_POS_MenuItemID 
LEFT JOIN POS_Products p 
ON p.POS_ProductID = mip.FK_ProductID
ORDER BY m.POS_MenuID, mi.POS_MenuItemID, mip.POS_MenuItemProductID, p.ProductName;
END
GO