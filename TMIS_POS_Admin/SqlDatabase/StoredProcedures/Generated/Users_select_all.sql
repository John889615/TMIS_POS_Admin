USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.Users_select_all', 'P') IS NOT NULL
    DROP PROCEDURE dbo.Users_select_all;
GO

CREATE PROCEDURE dbo.Users_select_all
AS
BEGIN
    SELECT *
    FROM Users;
END
GO