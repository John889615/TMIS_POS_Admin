USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.debtors_select_all_contacts', 'P') IS NOT NULL
    DROP PROCEDURE dbo.debtors_select_all_contacts;
GO

CREATE PROCEDURE dbo.debtors_select_all_contacts
@DebtorID INT

AS
BEGIN
    SELECT ec.EntityContactID
	 , e.EntityID
	 , c.ContactID
	 , ct.ContactTypeID
	 , ec.IsEmergency
	 , ec.IsMarketing
	 , ec.IsPrimary
	 , ec.PreferredContactTime
	 , ec.PreferredLanguageCode
	 , ec.ValidFrom
	 , ec.ValidTo
	 , dc.DialingCode
	 , c.ContactValue
	 , c.IsVerified
	 , c.VerificationToken
	 , c.VerifiedAt
	 , c.Notes
	 , ct.IsEmailType
	 , ct.IsPhoneNumberType
	 , ct.[Type] AS ContactType
	 , e.[Name] AS EntityName
FROM Debtors d
INNER JOIN EntityContacts ec
ON (d.DebtorID = ec.EntityRecordID)
INNER JOIN Contacts c
ON (ec.FK_ContactID = c.ContactID)
INNER JOIN ContactTypes ct
ON (c.FK_ContactTypeID= ct.ContactTypeID)
INNER JOIN Entities e
ON (ec.FK_EntityID = e.EntityID)
LEFT JOIN DialingCodes dc
ON (dc.DialingCodeID = c.FK_DialingCodeID)
WHERE DebtorID = @DebtorID
END
GO