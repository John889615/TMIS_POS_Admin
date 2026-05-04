USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.creditor_select_all_addresses', 'P') IS NOT NULL
    DROP PROCEDURE dbo.creditor_select_all_addresses;
GO

CREATE PROCEDURE dbo.creditor_select_all_addresses
@CreditorID INT,
@EntityID INT
AS
BEGIN
    SELECT ea.EntityAddressID
	 , e.EntityID
	 , a.AddressID
	 , att.AddressTypeID
	 , ea.IsPrimary
	 , ea.ValidFrom
	 , ea.ValidTo
	 , att.[Type] AS AddressType
	 , att.IsRequired
	 , att.CanEdit
	 , cp.ProvinceName
	 , ct.CountryName
	 , a.StreetAddress
	 , a.Locality
	 , a.PostalCode
	 , a.Landmark
	 , a.Latitude
	 , a.Longitude
	 , a.Notes
	 , e.[Name] AS EntityName
FROM Creditors c
LEFT JOIN EntityAddresses ea
ON (c.CreditorID = ea.EntityRecordID)
LEFT JOIN AddressTypes att
ON (ea.FK_AddressTypeID = att.AddressTypeID)
LEFT JOIN Addresses a
ON (ea.FK_AddressID = a.AddressID)
LEFT JOIN Entities e
ON (ea.FK_EntityID = e.EntityID)
LEFT JOIN Countries ct
ON (ct.CountryID = a.FK_CountryID)
LEFT JOIN CountryProvinces cp
ON (cp.CountryProvinceID = a.FK_ProvinceID)
WHERE CreditorID = @CreditorID
AND e.EntityID = @EntityID
END
GO