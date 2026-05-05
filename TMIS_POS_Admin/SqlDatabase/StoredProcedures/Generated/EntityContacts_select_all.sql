USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.EntityContacts_select_all', 'P') IS NOT NULL
    DROP PROCEDURE dbo.EntityContacts_select_all;
GO

CREATE PROCEDURE dbo.EntityContacts_select_all
AS
BEGIN
    SELECT *
    FROM EntityContacts;
END
GO