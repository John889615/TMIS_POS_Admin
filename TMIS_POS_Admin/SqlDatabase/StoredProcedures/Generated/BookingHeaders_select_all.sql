USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.BookingHeaders_select_all', 'P') IS NOT NULL
    DROP PROCEDURE dbo.BookingHeaders_select_all;
GO

CREATE PROCEDURE dbo.BookingHeaders_select_all
AS
BEGIN
    SELECT *
    FROM BookingHeaders;
END
GO