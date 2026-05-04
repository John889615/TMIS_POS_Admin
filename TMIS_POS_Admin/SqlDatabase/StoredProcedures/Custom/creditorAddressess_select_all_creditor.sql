USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.creditorAddressess_select_all_creditor', 'P') IS NOT NULL
    DROP PROCEDURE dbo.creditorAddressess_select_all_creditor;
GO

CREATE PROCEDURE dbo.creditorAddressess_select_all_creditor

AS
BEGIN
    SELECT a.*
FROM AddressTypes a
LEFT JOIN Entities e
ON (a.FK_EntityID = e.EntityID)
WHERE e.[Name] = 'Creditor'
END
GO