USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.EntityContacts_select_single', 'P') IS NOT NULL
    DROP PROCEDURE dbo.EntityContacts_select_single;
GO

CREATE PROCEDURE dbo.EntityContacts_select_single
    @EntityContactID INT
AS
BEGIN
    SELECT *
    FROM EntityContacts
    WHERE EntityContactID = @EntityContactID;
END
GO