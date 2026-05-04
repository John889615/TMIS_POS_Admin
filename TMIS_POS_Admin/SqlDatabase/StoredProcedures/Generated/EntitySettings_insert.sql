USE [TMIS_Development]
GO

IF OBJECT_ID('dbo.EntitySettings_insert', 'P') IS NOT NULL
    DROP PROCEDURE dbo.EntitySettings_insert;
GO

CREATE PROCEDURE dbo.EntitySettings_insert
    @FK_EntityID INT,
    @IsCreditor BIT = NULL,
    @IsDebtor BIT = NULL,
    @IsBranch BIT = NULL,
    @IsDepartment BIT = NULL,
    @IsUser BIT = NULL,
    @FK_CreatedUserID INT,
    @FK_UpdatedUserID INT = NULL,
    @DateCreated DATETIME,
    @DateUpdated DATETIME = NULL
AS
BEGIN
    DECLARE @Inserted TABLE (EntitySettingID INT);

    INSERT INTO EntitySettings (FK_EntityID, IsCreditor, IsDebtor, IsBranch, IsDepartment, IsUser, FK_CreatedUserID, FK_UpdatedUserID, DateCreated, DateUpdated)
    OUTPUT INSERTED.EntitySettingID INTO @Inserted
    VALUES (@FK_EntityID, @IsCreditor, @IsDebtor, @IsBranch, @IsDepartment, @IsUser, @FK_CreatedUserID, @FK_UpdatedUserID, @DateCreated, @DateUpdated);

    SELECT *
    FROM EntitySettings
    WHERE EntitySettingID = 
    (
        SELECT TOP 1 EntitySettingID
        FROM @Inserted
    );
END
GO