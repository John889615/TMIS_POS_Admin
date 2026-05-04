USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_MenuItems_select_single', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_MenuItems_select_single;
GO

CREATE PROCEDURE dbo.POS_MenuItems_select_single
    @MenuItemID INT
AS
BEGIN
    SELECT *
    FROM POS_MenuItems
    WHERE MenuItemID = @MenuItemID;
END
GO