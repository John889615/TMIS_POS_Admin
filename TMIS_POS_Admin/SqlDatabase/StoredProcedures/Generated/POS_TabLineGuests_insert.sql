USE [TMIS_Development]
GO

IF OBJECT_ID('dbo.POS_TabLineGuests_insert', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_TabLineGuests_insert;
GO

CREATE PROCEDURE dbo.POS_TabLineGuests_insert
    @FK_TabLineID UNIQUEIDENTIFIER,
    @FK_GuestID INT,
    @Note VARCHAR(MAX) = NULL,
    @DateUpdated DATETIME
AS
BEGIN
    DECLARE @Inserted TABLE (TabLineGuestID UNIQUEIDENTIFIER);

    INSERT INTO POS_TabLineGuests (FK_TabLineID, FK_GuestID, Note, DateUpdated)
    OUTPUT INSERTED.TabLineGuestID INTO @Inserted
    VALUES (@FK_TabLineID, @FK_GuestID, @Note, @DateUpdated);

    SELECT *
    FROM POS_TabLineGuests
    WHERE TabLineGuestID = 
    (
        SELECT TOP 1 TabLineGuestID
        FROM @Inserted
    );
END
GO