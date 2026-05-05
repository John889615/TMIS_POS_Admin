USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_AccountGuests_select_single', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_AccountGuests_select_single;
GO

CREATE PROCEDURE dbo.POS_AccountGuests_select_single
    @AccountGuestID UNIQUEIDENTIFIER
AS
BEGIN
    SELECT *
    FROM POS_AccountGuests
    WHERE AccountGuestID = @AccountGuestID;
END
GO