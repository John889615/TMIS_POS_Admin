USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.debtorMenuItemProducts_select_all_MenuItemID', 'P') IS NOT NULL
    DROP PROCEDURE dbo.debtorMenuItemProducts_select_all_MenuItemID;
GO

CREATE PROCEDURE dbo.debtorMenuItemProducts_select_all_MenuItemID
	@FK_MenuItemID INT
AS
BEGIN
    SELECT *
FROM POS_MenuItemProducts
WHERE FK_MenuItemID = @FK_MenuItemID
END
GO