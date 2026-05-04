USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.creditors_select_all_creditors', 'P') IS NOT NULL
    DROP PROCEDURE dbo.creditors_select_all_creditors;
GO

CREATE PROCEDURE dbo.creditors_select_all_creditors

AS
BEGIN
    SELECT c.CreditorID
		 , ctm.CreditorTypeMappingID
		 , ct.CreditorTypeID
		 , c.ShortCode
		 , c.[Name]
		 , cm.[Name] AS 'MasterCreditor'
		 , c.IsMasterCreditor
		 , ct.[Type] AS 'CreditorType'
		 , s.DisplayName AS 'Status'
	FROM Creditors c
	LEFT JOIN CreditorTypeMappings ctm
	ON (c.CreditorID = ctm.FK_CreditorID)
	LEFT JOIN Creditors cm
	ON (cm.CreditorID = c.FK_MasterCreditorID)
	LEFT JOIN CreditorTypes ct
	ON (ctm.FK_CreditorTypeID = ct.CreditorTypeID)
	LEFT JOIN Statuses s
	ON (ctm.FK_StatusID = s.StatusID)
END
GO