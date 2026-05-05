USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.Contacts_select_all', 'P') IS NOT NULL
    DROP PROCEDURE dbo.Contacts_select_all;
GO

CREATE PROCEDURE dbo.Contacts_select_all
AS
BEGIN
    SELECT *
    FROM Contacts;
END
GO