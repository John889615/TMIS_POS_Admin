USE [TMIS_Development]
GO

IF OBJECT_ID('dbo.POS_Settings_insert', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_Settings_insert;
GO

CREATE PROCEDURE dbo.POS_Settings_insert
    @CompanyName VARCHAR(255),
    @Email VARCHAR(255),
    @HeadOfficeNo VARCHAR(255),
    @FK_CurrencyID INT,
    @DateCreated DATETIME,
    @DateUpdated DATETIME = NULL
AS
BEGIN
    DECLARE @Inserted TABLE (SettingID INT);

    INSERT INTO POS_Settings (CompanyName, Email, HeadOfficeNo, FK_CurrencyID, DateCreated, DateUpdated)
    OUTPUT INSERTED.SettingID INTO @Inserted
    VALUES (@CompanyName, @Email, @HeadOfficeNo, @FK_CurrencyID, @DateCreated, @DateUpdated);

    SELECT *
    FROM POS_Settings
    WHERE SettingID = 
    (
        SELECT TOP 1 SettingID
        FROM @Inserted
    );
END
GO