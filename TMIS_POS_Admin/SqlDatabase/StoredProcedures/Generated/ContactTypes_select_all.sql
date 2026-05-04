USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.ContactTypes_select_all', 'P') IS NOT NULL
    DROP PROCEDURE dbo.ContactTypes_select_all;
GO

CREATE PROCEDURE dbo.ContactTypes_select_all
AS
BEGIN
    SELECT *
    FROM ContactTypes;
END
GO