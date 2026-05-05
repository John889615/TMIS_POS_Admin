USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.debtor_menu_update_status', 'P') IS NOT NULL
    DROP PROCEDURE dbo.debtor_menu_update_status;
GO

CREATE PROCEDURE dbo.debtor_menu_update_status
	@POS_DebtorMenuID INT
AS
BEGIN
    UPDATE POS_DebtorMenus
    SET IsActive = 0, DateUpdated = GETDATE()
    WHERE FK_CostCenterID IS NULL
      AND POS_DebtorMenuID <> @POS_DebtorMenuID;
END
GO