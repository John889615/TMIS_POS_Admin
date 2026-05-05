USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_Menus_select_single', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_Menus_select_single;
GO

CREATE PROCEDURE dbo.POS_Menus_select_single
    @MenuID INT
AS
BEGIN
    SELECT *
    FROM POS_Menus
    WHERE MenuID = @MenuID;
END
GO