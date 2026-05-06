USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.BookingGuests_update', 'P') IS NOT NULL
    DROP PROCEDURE dbo.BookingGuests_update;
GO

CREATE PROCEDURE dbo.BookingGuests_update
    @BookingGuestID INT,
    @FK_GuestID INT = NULL,
    @FK_BookingHeaderID INT,
    @DateCreated DATETIME,
    @DateUpdated DATETIME = NULL
AS
BEGIN
    UPDATE BookingGuests
    SET     FK_GuestID = @FK_GuestID,
    FK_BookingHeaderID = @FK_BookingHeaderID,
    DateUpdated = @DateUpdated
    WHERE BookingGuestID = @BookingGuestID;

    SELECT *
    FROM BookingGuests
    WHERE BookingGuestID = @BookingGuestID;
END
GO