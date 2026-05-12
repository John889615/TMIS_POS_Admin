USE [TMIS_Development]
GO

IF OBJECT_ID('dbo.AddressTypes_insert', 'P') IS NOT NULL
    DROP PROCEDURE dbo.AddressTypes_insert;
GO

CREATE PROCEDURE dbo.AddressTypes_insert
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
    DECLARE @Inserted TABLE (AddressTypeID INT);

    INSERT INTO AddressTypes (FK_EntityID, [Type], IsRequired, CanEdit, DateCreated, DateUpdated, FK_CreatedUserID, FK_UpdatedUserID)
    OUTPUT INSERTED.AddressTypeID INTO @Inserted
    VALUES (@FK_EntityID, @Type, @IsRequired, @CanEdit, @DateCreated, @DateUpdated, @FK_CreatedUserID, @FK_UpdatedUserID);

    SELECT *
    FROM AddressTypes
    WHERE AddressTypeID = 
    (
        SELECT TOP 1 AddressTypeID
        FROM @Inserted
    );
END
GO