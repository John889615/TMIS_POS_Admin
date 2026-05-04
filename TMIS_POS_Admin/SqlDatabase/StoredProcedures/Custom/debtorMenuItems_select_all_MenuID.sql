USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.debtorMenuItems_select_all_MenuID', 'P') IS NOT NULL
    DROP PROCEDURE dbo.debtorMenuItems_select_all_MenuID;
GO

CREATE PROCEDURE dbo.debtorMenuItems_select_all_MenuID
	@FK_MenuID INT
AS
BEGIN
    SELECT *
FROM POS_MenuItems
WHERE FK_MenuID = @FK_MenuID
END
GO