USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_TabLineGuests_select_single', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_TabLineGuests_select_single;
GO

CREATE PROCEDURE dbo.POS_TabLineGuests_select_single
    @TabLineGuestID UNIQUEIDENTIFIER
AS
BEGIN
    SELECT *
    FROM POS_TabLineGuests
    WHERE TabLineGuestID = @TabLineGuestID;
END
GO