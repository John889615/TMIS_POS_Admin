USE [TMIS_Development]
GO

IF OBJECT_ID('dbo.DialingCodes_insert', 'P') IS NOT NULL
    DROP PROCEDURE dbo.DialingCodes_insert;
GO

CREATE PROCEDURE dbo.DialingCodes_insert
    @DialingCode VARCHAR(10),
    @ISO2Code VARCHAR(2) = NULL,
    @FK_CreatedUserID INT = NULL,
    @FK_UpdatedUserID INT = NULL,
    @DateCreated DATETIME,
    @DateUpdated DATETIME = NULL
AS
BEGIN
    DECLARE @Inserted TABLE (DialingCodeID INT);

    INSERT INTO DialingCodes (DialingCode, ISO2Code, FK_CreatedUserID, FK_UpdatedUserID, DateCreated, DateUpdated)
    OUTPUT INSERTED.DialingCodeID INTO @Inserted
    VALUES (@DialingCode, @ISO2Code, @FK_CreatedUserID, @FK_UpdatedUserID, @DateCreated, @DateUpdated);

    SELECT *
    FROM DialingCodes
    WHERE DialingCodeID = 
    (
        SELECT TOP 1 DialingCodeID
        FROM @Inserted
    );
END
GO