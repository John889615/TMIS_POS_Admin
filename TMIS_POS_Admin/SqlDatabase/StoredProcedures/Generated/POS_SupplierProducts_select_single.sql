USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_SupplierProducts_select_single', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_SupplierProducts_select_single;
GO

CREATE PROCEDURE dbo.POS_SupplierProducts_select_single
    @SupplierProductID INT
AS
BEGIN
    SELECT *
    FROM POS_SupplierProducts
    WHERE SupplierProductID = @SupplierProductID;
END
GO