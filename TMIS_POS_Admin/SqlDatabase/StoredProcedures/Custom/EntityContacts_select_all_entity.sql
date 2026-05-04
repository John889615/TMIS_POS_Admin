USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.EntityContacts_select_all_entity', 'P') IS NOT NULL
    DROP PROCEDURE dbo.EntityContacts_select_all_entity;
GO

CREATE PROCEDURE dbo.EntityContacts_select_all_entity

@EntityID INT,
@EntityRecordID INT

AS
BEGIN
    SELECT ec.EntityContactID
		 , c.FK_ContactTypeID
		 , ec.FK_ContactID
		 , ec.IsPrimary
		 , ec.IsMarketing
		 , ec.IsEmergency
		 , ec.PreferredContactTime
		 , ec.PreferredLanguageCode
		 , ec.ValidFrom
		 , ec.ValidTo
		 , c.ContactValue
		 , c.FK_DialingCodeID
		 , c.IsVerified
		 , c.VerificationToken
		 , c.VerifiedAt
		 , c.Notes
  FROM EntityContacts ec
  INNER JOIN Contacts c
  ON (ec.FK_ContactID= c.ContactID)
  WHERE ec.FK_EntityID = @EntityID
  AND EntityRecordID = @EntityRecordID
END
GO