USE [TMIS_Development]
GO

IF OBJECT_ID('dbo.BookingHeaders_insert', 'P') IS NOT NULL
    DROP PROCEDURE dbo.BookingHeaders_insert;
GO

CREATE PROCEDURE dbo.BookingHeaders_insert
    @PartyName VARCHAR(150),
    @BookingReference VARCHAR(50),
    @TravelStart DATE = NULL,
    @TravelEnd DATE = NULL,
    @DateCreated DATETIME,
    @DateUpdated DATETIME = NULL,
    @IsStaffBooking BIT
AS
BEGIN
    DECLARE @Inserted TABLE (BookingHeaderID INT);

    INSERT INTO BookingHeaders (PartyName, BookingReference, TravelStart, TravelEnd, DateCreated, DateUpdated, IsStaffBooking)
    OUTPUT INSERTED.BookingHeaderID INTO @Inserted
    VALUES (@PartyName, @BookingReference, @TravelStart, @TravelEnd, @DateCreated, @DateUpdated, @IsStaffBooking);

    SELECT *
    FROM BookingHeaders
    WHERE BookingHeaderID = 
    (
        SELECT TOP 1 BookingHeaderID
        FROM @Inserted
    );
END
GO