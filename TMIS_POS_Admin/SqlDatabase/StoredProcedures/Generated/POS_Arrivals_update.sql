USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_Arrivals_update', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_Arrivals_update;
GO

CREATE PROCEDURE dbo.POS_Arrivals_update
    @ArrivalID UNIQUEIDENTIFIER,
    @FK_GuestID INT,
    @CheckedInBy VARCHAR(255) = NULL,
    @CheckInDate DATETIME,
    @CheckedOutBy VARCHAR(255) = NULL,
    @CheckOutDate DATETIME = NULL
AS
BEGIN
    UPDATE POS_Arrivals
    SET     FK_GuestID = @FK_GuestID,
    CheckedInBy = @CheckedInBy,
    CheckInDate = @CheckInDate,
    CheckedOutBy = @CheckedOutBy,
    CheckOutDate = @CheckOutDate
    WHERE ArrivalID = @ArrivalID;

    SELECT *
    FROM POS_Arrivals
    WHERE ArrivalID = @ArrivalID;
END
GO