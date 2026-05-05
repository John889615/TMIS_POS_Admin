USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.EntitySettings_update', 'P') IS NOT NULL
    DROP PROCEDURE dbo.EntitySettings_update;
GO

CREATE PROCEDURE dbo.EntitySettings_update
    @EntitySettingID INT,
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
    UPDATE EntitySettings
    SET     FK_EntityID = @FK_EntityID,
    IsCreditor = @IsCreditor,
    IsDebtor = @IsDebtor,
    IsBranch = @IsBranch,
    IsDepartment = @IsDepartment,
    IsUser = @IsUser,
    FK_CreatedUserID = @FK_CreatedUserID,
    FK_UpdatedUserID = @FK_UpdatedUserID,
    DateUpdated = @DateUpdated
    WHERE EntitySettingID = @EntitySettingID;

    SELECT *
    FROM EntitySettings
    WHERE EntitySettingID = @EntitySettingID;
END
GO