USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.EntityAddresses_select_all_entity', 'P') IS NOT NULL
    DROP PROCEDURE dbo.EntityAddresses_select_all_entity;
GO

CREATE PROCEDURE dbo.EntityAddresses_select_all_entity

@EntityID INT,
@EntityRecordID INT

AS
BEGIN
    SELECT ea.EntityAddressID
		 , ea.FK_AddressTypeID
		 , ea.FK_AddressID
		 , ea.IsPrimary
		 , ea.ValidFrom
		 , ea.ValidTo
		 , a.FK_CountryID
		 , a.FK_ProvinceID
		 , a.FK_AddressRegionID
		 , a.StreetAddress
		 , a.Locality
		 , a.PostalCode
		 , a.Landmark
		 , a.Latitude
		 , a.Longitude
		 , a.Notes
  FROM EntityAddresses ea
  INNER JOIN Addresses a
  ON (ea.FK_AddressID = a.AddressID)
  WHERE ea.FK_EntityID = @EntityID
  AND EntityRecordID = @EntityRecordID
END
GO