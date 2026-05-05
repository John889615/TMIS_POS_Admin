USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_MenuItemProducts_select_single', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_MenuItemProducts_select_single;
GO

CREATE PROCEDURE dbo.POS_MenuItemProducts_select_single
    @MenuItemProductID INT
AS
BEGIN
    SELECT *
    FROM POS_MenuItemProducts
    WHERE MenuItemProductID = @MenuItemProductID;
END
GO