USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_TabLines_select_single', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_TabLines_select_single;
GO

CREATE PROCEDURE dbo.POS_TabLines_select_single
    @TabLineID UNIQUEIDENTIFIER
AS
BEGIN
    SELECT *
    FROM POS_TabLines
    WHERE TabLineID = @TabLineID;
END
GO