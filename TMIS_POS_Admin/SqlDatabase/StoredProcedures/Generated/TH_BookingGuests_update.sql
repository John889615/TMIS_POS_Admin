USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.TH_BookingGuests_update', 'P') IS NOT NULL
    DROP PROCEDURE dbo.TH_BookingGuests_update;
GO

CREATE PROCEDURE dbo.TH_BookingGuests_update
    @BookingGuestID INT,
    @FK_BookingHeaderID INT,
    @FK_GuestID INT = NULL,
    @FK_CreatedUserID INT,
    @FK_UpdatedUserID INT = NULL,
    @DateCreated DATETIME,
    @DateUpdated DATETIME = NULL
AS
BEGIN
    UPDATE TH_BookingGuests
    SET     FK_BookingHeaderID = @FK_BookingHeaderID,
    FK_GuestID = @FK_GuestID,
    FK_CreatedUserID = @FK_CreatedUserID,
    FK_UpdatedUserID = @FK_UpdatedUserID,
    DateUpdated = @DateUpdated
    WHERE BookingGuestID = @BookingGuestID;

    SELECT *
    FROM TH_BookingGuests
    WHERE BookingGuestID = @BookingGuestID;
END
GO