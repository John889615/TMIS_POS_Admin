USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.Contacts_select_single', 'P') IS NOT NULL
    DROP PROCEDURE dbo.Contacts_select_single;
GO

CREATE PROCEDURE dbo.Contacts_select_single
    @ContactID INT
AS
BEGIN
    SELECT *
    FROM Contacts
    WHERE ContactID = @ContactID;
END
GO