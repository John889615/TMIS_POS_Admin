USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_AccountGuests_update', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_AccountGuests_update;
GO

CREATE PROCEDURE dbo.POS_AccountGuests_update
    @AccountGuestID UNIQUEIDENTIFIER,
    @FK_AccountID UNIQUEIDENTIFIER,
    @FK_GuestID INT,
    @IsResponsible BIT,
    @DateCreated DATETIME,
    @DateUpdated DATETIME = NULL
AS
BEGIN
    UPDATE POS_AccountGuests
    SET     FK_AccountID = @FK_AccountID,
    FK_GuestID = @FK_GuestID,
    IsResponsible = @IsResponsible,
    DateUpdated = @DateUpdated
    WHERE AccountGuestID = @AccountGuestID;

    SELECT *
    FROM POS_AccountGuests
    WHERE AccountGuestID = @AccountGuestID;
END
GO