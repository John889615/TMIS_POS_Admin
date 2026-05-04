USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.costCenterProducts_select_all_costCenterProducts', 'P') IS NOT NULL
    DROP PROCEDURE dbo.costCenterProducts_select_all_costCenterProducts;
GO

CREATE PROCEDURE dbo.costCenterProducts_select_all_costCenterProducts
	@FK_CostCenterID INT
AS
BEGIN
    SELECT ccp.CostCenterProductID
	 , ccp.FK_ProductID
	 , p.ProductName
	 , ccp.FK_CostCenterID
	 , cc.[Name] AS CostCenter
	 , ccp.FK_TaxTypeID
	 , tt.TaxPercentage AS Rate
	 , ccp.[Value]
	 , ccp.Vat
	 , ccp.ItemPrice
	 , ccp.FK_SellUnitID
	 , u.Symbol
	 , u.Unit
	 , ccp.QuantityOnHand
	 , ccp.IsAvailable
	 , ccp.IsActive
	 , cu.Firstname + ' ' + cu.Lastname AS CreatedBy
	 , uu.Firstname + ' ' + uu.Lastname AS UpdatedBy
FROM POS_CostCenterProducts ccp
INNER JOIN POS_Products p
ON (p.ProductID = ccp.FK_ProductID)
INNER JOIN POS_CostCenters cc
ON (cc.CostCenterID = ccp.FK_CostCenterID)
LEFT JOIN POS_TaxTypes tt
ON (tt.TaxTypeID = ccp.FK_TaxTypeID)
INNER JOIN POS_Units u
ON (u.UnitID = ccp.FK_SellUnitID)
INNER JOIN Users cu
ON (cu.UserID = ccp.FK_CreatedUserID)
INNER JOIN Users uu
ON (uu.UserID = ccp.FK_UpdatedUserID)
WHERE ccp.FK_CostCenterID = @FK_CostCenterID
END
GO