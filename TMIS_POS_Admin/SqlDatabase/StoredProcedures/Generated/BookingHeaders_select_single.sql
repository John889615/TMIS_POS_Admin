USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.BookingHeaders_select_single', 'P') IS NOT NULL
    DROP PROCEDURE dbo.BookingHeaders_select_single;
GO

CREATE PROCEDURE dbo.BookingHeaders_select_single
    @BookingHeaderID INT
AS
BEGIN
    SELECT *
    FROM BookingHeaders
    WHERE BookingHeaderID = @BookingHeaderID;
END
GO