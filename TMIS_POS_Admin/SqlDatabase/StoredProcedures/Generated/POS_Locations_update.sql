USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_Locations_update', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_Locations_update;
GO

CREATE PROCEDURE dbo.POS_Locations_update
    @LocationID INT,
    @FK_CurrencyID INT,
    @BC_ID VARCHAR(255) = NULL,
    @ShortCode VARCHAR(50) = NULL,
    @Name VARCHAR(255),
    @IsActive BIT,
    @DateCreated DATETIME,
    @FK_CreatedUserID INT,
    @DateUpdated DATETIME,
    @FK_UpdatedUserID INT,
    @ContactEmail NVARCHAR(200) = NULL,
    @SupportEmail NVARCHAR(200) = NULL,
    @LastSyncSeenAt DATETIME2 = NULL,
    @SilentAlertSentAt DATETIME2 = NULL
AS
BEGIN
    UPDATE POS_Locations
    SET     FK_CurrencyID = @FK_CurrencyID,
    BC_ID = @BC_ID,
    ShortCode = @ShortCode,
    [Name] = @Name,
    IsActive = @IsActive,
    FK_CreatedUserID = @FK_CreatedUserID,
    DateUpdated = @DateUpdated,
    FK_UpdatedUserID = @FK_UpdatedUserID,
    ContactEmail = @ContactEmail,
    SupportEmail = @SupportEmail,
    LastSyncSeenAt = @LastSyncSeenAt,
    SilentAlertSentAt = @SilentAlertSentAt
    WHERE LocationID = @LocationID;

    SELECT *
    FROM POS_Locations
    WHERE LocationID = @LocationID;
END
GO