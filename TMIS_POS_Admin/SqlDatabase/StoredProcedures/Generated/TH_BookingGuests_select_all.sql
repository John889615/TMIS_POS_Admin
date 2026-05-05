USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.TH_BookingGuests_select_all', 'P') IS NOT NULL
    DROP PROCEDURE dbo.TH_BookingGuests_select_all;
GO

CREATE PROCEDURE dbo.TH_BookingGuests_select_all
AS
BEGIN
    SELECT *
    FROM TH_BookingGuests;
END
GO