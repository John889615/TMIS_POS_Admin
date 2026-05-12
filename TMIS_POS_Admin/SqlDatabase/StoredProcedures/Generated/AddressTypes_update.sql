USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.AddressTypes_update', 'P') IS NOT NULL
    DROP PROCEDURE dbo.AddressTypes_update;
GO

CREATE PROCEDURE dbo.AddressTypes_update
    @AddressTypeID INT,
    @FK_EntityID INT,
    @Type VARCHAR(255),
    @IsRequired BIT,
    @CanEdit BIT,
    @DateCreated DATETIME = NULL,
    @DateUpdated DATETIME = NULL,
    @FK_CreatedUserID INT = NULL,
    @FK_UpdatedUserID INT = NULL
AS
BEGIN
    UPDATE AddressTypes
    SET     FK_EntityID = @FK_EntityID,
    [Type] = @Type,
    IsRequired = @IsRequired,
    CanEdit = @CanEdit,
    DateUpdated = @DateUpdated,
    FK_CreatedUserID = @FK_CreatedUserID,
    FK_UpdatedUserID = @FK_UpdatedUserID
    WHERE AddressTypeID = @AddressTypeID;

    SELECT *
    FROM AddressTypes
    WHERE AddressTypeID = @AddressTypeID;
END
GO