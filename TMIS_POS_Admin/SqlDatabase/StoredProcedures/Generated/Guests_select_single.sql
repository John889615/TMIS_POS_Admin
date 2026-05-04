USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.Guests_select_single', 'P') IS NOT NULL
    DROP PROCEDURE dbo.Guests_select_single;
GO

CREATE PROCEDURE dbo.Guests_select_single
    @GuestID INT
AS
BEGIN
    SELECT *
    FROM Guests
    WHERE GuestID = @GuestID;
END
GO