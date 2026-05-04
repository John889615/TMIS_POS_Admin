USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.DialingCodes_select_single', 'P') IS NOT NULL
    DROP PROCEDURE dbo.DialingCodes_select_single;
GO

CREATE PROCEDURE dbo.DialingCodes_select_single
    @DialingCodeID INT
AS
BEGIN
    SELECT *
    FROM DialingCodes
    WHERE DialingCodeID = @DialingCodeID;
END
GO