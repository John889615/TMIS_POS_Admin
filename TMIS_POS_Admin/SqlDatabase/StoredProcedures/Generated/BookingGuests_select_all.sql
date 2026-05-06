USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.BookingGuests_select_all', 'P') IS NOT NULL
    DROP PROCEDURE dbo.BookingGuests_select_all;
GO

CREATE PROCEDURE dbo.BookingGuests_select_all
AS
BEGIN
    SELECT *
    FROM BookingGuests;
END
GO