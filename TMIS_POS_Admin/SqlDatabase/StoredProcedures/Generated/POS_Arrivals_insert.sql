USE [TMIS_Development]
GO

IF OBJECT_ID('dbo.POS_Arrivals_insert', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_Arrivals_insert;
GO

CREATE PROCEDURE dbo.POS_Arrivals_insert
    @ArrivalID UNIQUEIDENTIFIER = NULL,
    @FK_GuestID INT,
    @CheckedInBy VARCHAR(255) = NULL,
    @CheckInDate DATETIME,
    @CheckedOutBy VARCHAR(255) = NULL,
    @CheckOutDate DATETIME = NULL
AS
BEGIN
    DECLARE @Inserted TABLE (ArrivalID UNIQUEIDENTIFIER);

    INSERT INTO POS_Arrivals (ArrivalID, FK_GuestID, CheckedInBy, CheckInDate, CheckedOutBy, CheckOutDate)
    OUTPUT INSERTED.ArrivalID INTO @Inserted
    VALUES (ISNULL(@ArrivalID, NEWID()), @FK_GuestID, @CheckedInBy, @CheckInDate, @CheckedOutBy, @CheckOutDate);

    SELECT *
    FROM POS_Arrivals
    WHERE ArrivalID = 
    (
        SELECT TOP 1 ArrivalID
        FROM @Inserted
    );
END
GO