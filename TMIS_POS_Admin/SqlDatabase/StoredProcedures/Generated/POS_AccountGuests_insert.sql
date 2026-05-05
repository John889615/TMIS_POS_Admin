USE [TMIS_Development]
GO

IF OBJECT_ID('dbo.POS_AccountGuests_insert', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_AccountGuests_insert;
GO

CREATE PROCEDURE dbo.POS_AccountGuests_insert
    @FK_AccountID UNIQUEIDENTIFIER,
    @FK_GuestID INT,
    @IsResponsible BIT,
    @DateCreated DATETIME,
    @DateUpdated DATETIME = NULL
AS
BEGIN
    DECLARE @Inserted TABLE (AccountGuestID UNIQUEIDENTIFIER);

    INSERT INTO POS_AccountGuests (FK_AccountID, FK_GuestID, IsResponsible, DateCreated, DateUpdated)
    OUTPUT INSERTED.AccountGuestID INTO @Inserted
    VALUES (@FK_AccountID, @FK_GuestID, @IsResponsible, @DateCreated, @DateUpdated);

    SELECT *
    FROM POS_AccountGuests
    WHERE AccountGuestID = 
    (
        SELECT TOP 1 AccountGuestID
        FROM @Inserted
    );
END
GO