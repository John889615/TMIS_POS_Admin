USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.BookingHeaders_update', 'P') IS NOT NULL
    DROP PROCEDURE dbo.BookingHeaders_update;
GO

CREATE PROCEDURE dbo.BookingHeaders_update
    @BookingHeaderID INT,
    @PartyName VARCHAR(150),
    @BookingReference VARCHAR(50),
    @TravelStart DATE = NULL,
    @TravelEnd DATE = NULL,
    @DateCreated DATETIME,
    @DateUpdated DATETIME = NULL,
    @IsStaffBooking BIT
AS
BEGIN
    UPDATE BookingHeaders
    SET     PartyName = @PartyName,
    BookingReference = @BookingReference,
    TravelStart = @TravelStart,
    TravelEnd = @TravelEnd,
    DateUpdated = @DateUpdated,
    IsStaffBooking = @IsStaffBooking
    WHERE BookingHeaderID = @BookingHeaderID;

    SELECT *
    FROM BookingHeaders
    WHERE BookingHeaderID = @BookingHeaderID;
END
GO