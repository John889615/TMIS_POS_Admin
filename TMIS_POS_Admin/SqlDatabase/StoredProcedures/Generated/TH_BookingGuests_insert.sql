USE [TMIS_Development]
GO

IF OBJECT_ID('dbo.TH_BookingGuests_insert', 'P') IS NOT NULL
    DROP PROCEDURE dbo.TH_BookingGuests_insert;
GO

CREATE PROCEDURE dbo.TH_BookingGuests_insert
    @FK_BookingHeaderID INT,
    @FK_GuestID INT = NULL,
    @FK_CreatedUserID INT,
    @FK_UpdatedUserID INT = NULL,
    @DateCreated DATETIME,
    @DateUpdated DATETIME = NULL
AS
BEGIN
    DECLARE @Inserted TABLE (BookingGuestID INT);

    INSERT INTO TH_BookingGuests (FK_BookingHeaderID, FK_GuestID, FK_CreatedUserID, FK_UpdatedUserID, DateCreated, DateUpdated)
    OUTPUT INSERTED.BookingGuestID INTO @Inserted
    VALUES (@FK_BookingHeaderID, @FK_GuestID, @FK_CreatedUserID, @FK_UpdatedUserID, @DateCreated, @DateUpdated);

    SELECT *
    FROM TH_BookingGuests
    WHERE BookingGuestID = 
    (
        SELECT TOP 1 BookingGuestID
        FROM @Inserted
    );
END
GO