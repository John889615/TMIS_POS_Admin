USE [TMIS_Development]
GO

IF OBJECT_ID('dbo.BookingGuests_insert', 'P') IS NOT NULL
    DROP PROCEDURE dbo.BookingGuests_insert;
GO

CREATE PROCEDURE dbo.BookingGuests_insert
    @FK_GuestID INT = NULL,
    @FK_BookingHeaderID INT,
    @DateCreated DATETIME,
    @DateUpdated DATETIME = NULL
AS
BEGIN
    DECLARE @Inserted TABLE (BookingGuestID INT);

    INSERT INTO BookingGuests (FK_GuestID, FK_BookingHeaderID, DateCreated, DateUpdated)
    OUTPUT INSERTED.BookingGuestID INTO @Inserted
    VALUES (@FK_GuestID, @FK_BookingHeaderID, @DateCreated, @DateUpdated);

    SELECT *
    FROM BookingGuests
    WHERE BookingGuestID = 
    (
        SELECT TOP 1 BookingGuestID
        FROM @Inserted
    );
END
GO