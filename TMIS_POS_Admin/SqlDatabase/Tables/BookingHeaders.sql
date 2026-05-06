USE [TMIS_Development]
GO

IF OBJECT_ID('BookingHeaders', 'U') IS NOT NULL
	DROP TABLE BookingHeaders
GO

CREATE TABLE BookingHeaders
(
    BookingHeaderID    INT          NOT NULL PRIMARY KEY,
    PartyName          VARCHAR(150) NOT NULL,
    BookingReference   VARCHAR(50)  NOT NULL,
    TravelStart        DATE         NULL,
    TravelEnd          DATE         NULL,
    DateCreated        DATETIME     NOT NULL DEFAULT GETDATE(),
    DateUpdated        DATETIME     NULL,
    IsStaffBooking     BIT          NOT NULL CONSTRAINT DF_BookingHeaders_IsStaffBooking DEFAULT 0,

    CONSTRAINT UQ_BookingHeaders_BookingReference UNIQUE (BookingReference)
)
