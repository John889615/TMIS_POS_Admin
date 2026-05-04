USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_CostCenters_select_single', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_CostCenters_select_single;
GO

CREATE PROCEDURE dbo.POS_CostCenters_select_single
    @CostCenterID INT
AS
BEGIN
    SELECT *
    FROM POS_CostCenters
    WHERE CostCenterID = @CostCenterID;
END
GO