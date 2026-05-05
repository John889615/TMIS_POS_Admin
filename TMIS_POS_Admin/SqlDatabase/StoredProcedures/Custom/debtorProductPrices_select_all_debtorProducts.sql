USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.debtorProductPrices_select_all_debtorProducts', 'P') IS NOT NULL
    DROP PROCEDURE dbo.debtorProductPrices_select_all_debtorProducts;
GO

CREATE PROCEDURE dbo.debtorProductPrices_select_all_debtorProducts
	@FK_DebtorProductID INT
AS
BEGIN
    SELECT *
FROM POS_DebtorProductPrices 
WHERE FK_DebtorProductID = @FK_DebtorProductID
END
GO