USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_CostCenterProducts_select_single', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_CostCenterProducts_select_single;
GO

CREATE PROCEDURE dbo.POS_CostCenterProducts_select_single
    @CostCenterProductID INT
AS
BEGIN
    SELECT *
    FROM POS_CostCenterProducts
    WHERE CostCenterProductID = @CostCenterProductID;
END
GO