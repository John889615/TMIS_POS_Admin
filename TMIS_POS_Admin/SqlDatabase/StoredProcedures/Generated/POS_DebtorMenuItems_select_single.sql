USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_DebtorMenuItems_select_single', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_DebtorMenuItems_select_single;
GO

CREATE PROCEDURE dbo.POS_DebtorMenuItems_select_single
    @DebtorMenuItemID INT
AS
BEGIN
    SELECT *
    FROM POS_DebtorMenuItems
    WHERE DebtorMenuItemID = @DebtorMenuItemID;
END
GO