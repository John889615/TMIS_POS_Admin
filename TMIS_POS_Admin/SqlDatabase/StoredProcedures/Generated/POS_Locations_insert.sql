USE [TMIS_Development]
GO

IF OBJECT_ID('dbo.POS_Locations_insert', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_Locations_insert;
GO

CREATE PROCEDURE dbo.POS_Locations_insert
    @FK_CurrencyID INT,
    @BC_ID VARCHAR(255) = NULL,
    @ShortCode VARCHAR(50) = NULL,
    @Name VARCHAR(255),
    @IsActive BIT,
    @DateCreated DATETIME,
    @FK_CreatedUserID INT,
    @DateUpdated DATETIME = NULL,
    @FK_UpdatedUserID INT,
    @ContactEmail NVARCHAR(200) = NULL,
    @SupportEmail NVARCHAR(200) = NULL,
    @LastSyncSeenAt DATETIME2 = NULL,
    @SilentAlertSentAt DATETIME2 = NULL
AS
BEGIN
    DECLARE @Inserted TABLE (LocationID INT);

    INSERT INTO POS_Locations (FK_CurrencyID, BC_ID, ShortCode, [Name], IsActive, DateCreated, FK_CreatedUserID, DateUpdated, FK_UpdatedUserID, ContactEmail, SupportEmail, LastSyncSeenAt, SilentAlertSentAt)
    OUTPUT INSERTED.LocationID INTO @Inserted
    VALUES (@FK_CurrencyID, @BC_ID, @ShortCode, @Name, @IsActive, @DateCreated, @FK_CreatedUserID, @DateUpdated, @FK_UpdatedUserID, @ContactEmail, @SupportEmail, @LastSyncSeenAt, @SilentAlertSentAt);

    SELECT *
    FROM POS_Locations
    WHERE LocationID = 
    (
        SELECT TOP 1 LocationID
        FROM @Inserted
    );
END
GO