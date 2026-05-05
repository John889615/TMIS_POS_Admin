USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.debtor_menu_update_status_cost_center', 'P') IS NOT NULL
    DROP PROCEDURE dbo.debtor_menu_update_status_cost_center;
GO

-- exec CostCenters_select_all_LocationID 1

CREATE PROCEDURE dbo.debtor_menu_update_status_cost_center
	@POS_DebtorMenuID INT,
    @FK_CostCenterID INT
AS
BEGIN
    UPDATE POS_DebtorMenus
    SET IsActive = 0, DateUpdated = GETDATE()
    WHERE FK_CostCenterID = @FK_CostCenterID
      AND POS_DebtorMenuID <> @POS_DebtorMenuID;
END
GO