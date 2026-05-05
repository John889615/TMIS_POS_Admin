USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.Guests_select_all', 'P') IS NOT NULL
    DROP PROCEDURE dbo.Guests_select_all;
GO

CREATE PROCEDURE dbo.Guests_select_all
AS
BEGIN
    SELECT *
    FROM Guests;
END
GO