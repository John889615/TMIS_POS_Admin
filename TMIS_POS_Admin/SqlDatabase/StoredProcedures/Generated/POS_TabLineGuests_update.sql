USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_TabLineGuests_update', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_TabLineGuests_update;
GO

CREATE PROCEDURE dbo.POS_TabLineGuests_update
    @TabLineGuestID UNIQUEIDENTIFIER,
    @FK_TabLineID UNIQUEIDENTIFIER,
    @FK_GuestID INT,
    @Note VARCHAR(MAX) = NULL,
    @DateUpdated DATETIME
AS
BEGIN
    UPDATE POS_TabLineGuests
    SET     FK_TabLineID = @FK_TabLineID,
    FK_GuestID = @FK_GuestID,
    Note = @Note,
    DateUpdated = @DateUpdated
    WHERE TabLineGuestID = @TabLineGuestID;

    SELECT *
    FROM POS_TabLineGuests
    WHERE TabLineGuestID = @TabLineGuestID;
END
GO