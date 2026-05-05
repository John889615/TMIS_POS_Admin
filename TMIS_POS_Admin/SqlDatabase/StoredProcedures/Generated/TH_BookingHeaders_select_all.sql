USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.TH_BookingHeaders_select_all', 'P') IS NOT NULL
    DROP PROCEDURE dbo.TH_BookingHeaders_select_all;
GO

CREATE PROCEDURE dbo.TH_BookingHeaders_select_all
AS
BEGIN
    SELECT *
    FROM TH_BookingHeaders;
END
GO