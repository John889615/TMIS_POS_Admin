USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_CostCenterTypes_select_single', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_CostCenterTypes_select_single;
GO

CREATE PROCEDURE dbo.POS_CostCenterTypes_select_single
    @CostCenterTypeID INT
AS
BEGIN
    SELECT *
    FROM POS_CostCenterTypes
    WHERE CostCenterTypeID = @CostCenterTypeID;
END
GO