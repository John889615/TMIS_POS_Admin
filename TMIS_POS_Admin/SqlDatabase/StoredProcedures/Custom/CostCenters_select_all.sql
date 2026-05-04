USE [TMIS_BlueSafaris]
GO


IF OBJECT_ID('dbo.CostCenters_select_all', 'P') IS NOT NULL
    DROP PROCEDURE dbo.CostCenters_select_all;
GO

CREATE PROCEDURE dbo.CostCenters_select_all

AS
BEGIN
    SELECT cc.CostCenterID
	 , cc.[Name]
	 , cc.FK_LocationID
	 , l.[Name] AS Debtor
	 , cc.FK_StatusID
	 , s.DisplayName AS [Status]
	 , cc.FK_CostCenterTypeID
	 , cct.[Name] AS [Type]
	 , cc.BillingReference
FROM POS_CostCenters cc
INNER JOIN POS_CostCenterTypes cct
ON (cc.FK_CostCenterTypeID = cct.CostCenterTypeID)
INNER JOIN POS_Locations l
ON (l.LocationID = cc.FK_LocationID)
INNER JOIN Statuses s
ON (s.StatusID = cc.FK_StatusID)
END
GO