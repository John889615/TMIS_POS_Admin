USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.TH_BookingHeaders_select_single', 'P') IS NOT NULL
    DROP PROCEDURE dbo.TH_BookingHeaders_select_single;
GO

CREATE PROCEDURE dbo.TH_BookingHeaders_select_single

AS
BEGIN
    SELECT *
    FROM TH_BookingHeaders
    WHERE ;
END
GO