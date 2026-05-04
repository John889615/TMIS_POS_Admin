USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.debtors_select_all_debtors', 'P') IS NOT NULL
    DROP PROCEDURE dbo.debtors_select_all_debtors;
GO

CREATE PROCEDURE dbo.debtors_select_all_debtors

AS
BEGIN
    SELECT d.DebtorID
		 , dtm.DebtorTypeMappingID
		 , d.ShortCode
		 , d.[Name]
		 , dm.DebtorID AS 'MasterDebtorID'
		 , dm.[Name] AS 'MasterDebtor'
		 , d.IsMasterDebtor
		 , dt.DebtorTypeID
		 , dt.[Type] AS 'DebtorType'
		 , s.StatusID
		 , s.DisplayName AS 'Status'
		 , b.BranchID
		 , b.[Name] AS 'Branch'
		 , dept.DepartmentID
		 , dept.[Name] AS 'Department'
	FROM Debtors d
	LEFT JOIN DebtorTypeMappings dtm
	ON (d.DebtorID = dtm.FK_DebtorID)
	LEFT JOIN Debtors dm
	ON (dm.DebtorID = d.FK_MasterDebtorID)
	LEFT JOIN DebtorTypes dt
	ON (dtm.FK_DebtorTypeID = dt.DebtorTypeID)
	LEFT JOIN Statuses s
	ON (dtm.FK_StatusID = s.StatusID)
	LEFT JOIN Branches b
	ON (dtm.FK_BranchID = b.BranchID)
	LEFT JOIN Departments dept
	ON (dtm.FK_DepartmentID = dept.DepartmentID)
END
GO