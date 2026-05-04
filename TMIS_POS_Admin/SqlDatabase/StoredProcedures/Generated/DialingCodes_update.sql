USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.DialingCodes_update', 'P') IS NOT NULL
    DROP PROCEDURE dbo.DialingCodes_update;
GO

CREATE PROCEDURE dbo.DialingCodes_update
    @DialingCodeID INT,
    @DialingCode VARCHAR(10),
    @ISO2Code VARCHAR(2) = NULL
AS
BEGIN
    UPDATE DialingCodes
    SET     DialingCode = @DialingCode,
    ISO2Code = @ISO2Code
    WHERE DialingCodeID = @DialingCodeID;

    SELECT *
    FROM DialingCodes
    WHERE DialingCodeID = @DialingCodeID;
END
GO