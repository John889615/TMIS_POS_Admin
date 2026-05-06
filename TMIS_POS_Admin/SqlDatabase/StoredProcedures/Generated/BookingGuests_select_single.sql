USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.BookingGuests_select_single', 'P') IS NOT NULL
    DROP PROCEDURE dbo.BookingGuests_select_single;
GO

CREATE PROCEDURE dbo.BookingGuests_select_single
    @BookingGuestID INT
AS
BEGIN
    SELECT *
    FROM BookingGuests
    WHERE BookingGuestID = @BookingGuestID;
END
GO