USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.debtorAddressess_select_all_debtor', 'P') IS NOT NULL
    DROP PROCEDURE dbo.debtorAddressess_select_all_debtor;
GO

CREATE PROCEDURE dbo.debtorAddressess_select_all_debtor

AS
BEGIN
    SELECT a.*
FROM AddressTypes a
LEFT JOIN Entities e
ON (a.FK_EntityID = e.EntityID)
WHERE e.[Name] = 'Locations'
END
GO