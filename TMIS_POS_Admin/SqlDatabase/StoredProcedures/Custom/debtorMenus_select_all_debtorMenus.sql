USE [TMIS_BlueSafaris]
GO


IF OBJECT_ID('dbo.debtorMenus_select_all_debtorMenus', 'P') IS NOT NULL
    DROP PROCEDURE dbo.debtorMenus_select_all_debtorMenus;
GO

-- exec debtorMenus_select_all_debtorMenus 1

CREATE PROCEDURE dbo.debtorMenus_select_all_debtorMenus
	@FK_LocationID INT
AS
BEGIN
     SELECT 
        dm.DebtorMenuID AS MenuID,
        dm.MenuName,
        dm.DateCreated,
        dm.DateUpdated,
        'Camp' AS SourceType,
		l.[Name] AS [Location],
        dm.ValidFrom,
        dm.ValidTo,
		dm.IsActive,
		i.ImageUrl
    FROM POS_DebtorMenus dm
	LEFT JOIN POS_Locations l
	ON l.LocationID = dm.FK_LocationID
	LEFT JOIN POS_Images i
	ON i.FK_ItemID = dm.DebtorMenuID
	AND i.RelativePath = 'debtor_menus' 
    WHERE dm.FK_LocationID = @FK_LocationID

    UNION ALL

    SELECT 
        pm.MenuID AS MenuID,
        pm.MenuName,
        pm.DateCreated,
        pm.DateUpdated,
        'Global' AS SourceType,
		NULL AS [Location],
        NULL AS ValidFrom,
        NULL AS ValidTo,
		pm.IsActive,
		i.ImageUrl
    FROM POS_Menus pm
	LEFT JOIN POS_Images i
	ON i.FK_ItemID = pm.MenuID
	AND i.RelativePath = 'menus'
END
GO