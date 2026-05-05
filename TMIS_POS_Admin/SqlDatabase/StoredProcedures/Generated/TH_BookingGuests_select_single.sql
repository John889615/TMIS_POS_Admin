USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.TH_BookingGuests_select_single', 'P') IS NOT NULL
    DROP PROCEDURE dbo.TH_BookingGuests_select_single;
GO

CREATE PROCEDURE dbo.TH_BookingGuests_select_single
    @BookingGuestID INT
AS
BEGIN
    SELECT *
    FROM TH_BookingGuests
    WHERE BookingGuestID = @BookingGuestID;
END
GO