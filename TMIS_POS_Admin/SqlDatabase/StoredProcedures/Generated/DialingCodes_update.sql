USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.DialingCodes_update', 'P') IS NOT NULL
    DROP PROCEDURE dbo.DialingCodes_update;
GO

CREATE PROCEDURE dbo.DialingCodes_update
    @DialingCodeID INT,
    @DialingCode VARCHAR(10),
    @ISO2Code VARCHAR(2) = NULL,
    @FK_CreatedUserID INT = NULL,
    @FK_UpdatedUserID INT = NULL,
    @DateCreated DATETIME,
    @DateUpdated DATETIME = NULL
AS
BEGIN
    UPDATE DialingCodes
    SET     DialingCode = @DialingCode,
    ISO2Code = @ISO2Code,
    FK_CreatedUserID = @FK_CreatedUserID,
    FK_UpdatedUserID = @FK_UpdatedUserID,
    DateUpdated = @DateUpdated
    WHERE DialingCodeID = @DialingCodeID;

    SELECT *
    FROM DialingCodes
    WHERE DialingCodeID = @DialingCodeID;
END
GO