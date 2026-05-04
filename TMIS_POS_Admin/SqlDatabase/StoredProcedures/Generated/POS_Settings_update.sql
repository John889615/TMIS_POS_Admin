USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_Settings_update', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_Settings_update;
GO

CREATE PROCEDURE dbo.POS_Settings_update
    @SettingID INT,
    @CompanyName VARCHAR(255),
    @Email VARCHAR(255),
    @HeadOfficeNo VARCHAR(255),
    @FK_CurrencyID INT,
    @DateCreated DATETIME,
    @DateUpdated DATETIME
AS
BEGIN
    UPDATE POS_Settings
    SET     CompanyName = @CompanyName,
    Email = @Email,
    HeadOfficeNo = @HeadOfficeNo,
    FK_CurrencyID = @FK_CurrencyID,
    DateUpdated = @DateUpdated
    WHERE SettingID = @SettingID;

    SELECT *
    FROM POS_Settings
    WHERE SettingID = @SettingID;
END
GO