USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.EntityContact_select_contactID', 'P') IS NOT NULL
    DROP PROCEDURE dbo.EntityContact_select_contactID;
GO

CREATE PROCEDURE dbo.EntityContact_select_contactID
	@FK_ContactID INT
AS
BEGIN
    SELECT *
	FROM EntityContacts
	WHERE FK_ContactID = @FK_ContactID
END
GO