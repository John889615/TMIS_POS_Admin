USE [TMIS_Development]
GO

IF OBJECT_ID('BookingGuests', 'U') IS NOT NULL
	DROP TABLE BookingGuests
GO

CREATE TABLE BookingGuests
(
    BookingGuestID       INT      NOT NULL PRIMARY KEY,
    FK_GuestID           INT      NULL FOREIGN KEY REFERENCES Guests(GuestID),
    FK_BookingHeaderID   INT      NOT NULL FOREIGN KEY REFERENCES BookingHeaders(BookingHeaderID),
    DateCreated          DATETIME NOT NULL DEFAULT GETDATE(),
    DateUpdated          DATETIME NULL
)
