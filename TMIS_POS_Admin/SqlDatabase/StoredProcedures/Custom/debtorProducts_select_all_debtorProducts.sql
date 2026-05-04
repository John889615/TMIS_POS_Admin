USE [TMIS_Development]
GO

-- exec debtorProducts_select_all_debtorProducts 5

IF OBJECT_ID('dbo.debtorProducts_select_all_debtorProducts', 'P') IS NOT NULL
    DROP PROCEDURE dbo.debtorProducts_select_all_debtorProducts;
GO

CREATE PROCEDURE dbo.debtorProducts_select_all_debtorProducts
	@FK_LocationID INT
AS
BEGIN
    SELECT dp.DebtorProductID
	 , dp.FK_ProductID
	 , p.ProductName
	 , dp.FK_LocationID
	 , l.[Name] AS Debtor
	 , dp.FK_SellUnitID
	 , u.Symbol
	 , u.Unit
	 , dp.QuantityOnHand
	 , dp.IsAvailable
	 , dp.IsActive
FROM POS_DebtorProducts dp
INNER JOIN POS_Products p
ON (p.ProductID = dp.FK_ProductID)
INNER JOIN POS_Locations l
ON (l.LocationID = dp.FK_LocationID)
INNER JOIN POS_Units u
ON (u.UnitID = dp.FK_SellUnitID)
INNER JOIN Users cu
ON (cu.UserID = dp.FK_CreatedUserID)
INNER JOIN Users uu
ON (uu.UserID = dp.FK_UpdatedUserID)
WHERE dp.FK_LocationID = @FK_LocationID
END
GO