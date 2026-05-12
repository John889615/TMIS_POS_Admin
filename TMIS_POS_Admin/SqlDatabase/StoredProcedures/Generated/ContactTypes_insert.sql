USE [TMIS_Development]
GO

IF OBJECT_ID('dbo.ContactTypes_insert', 'P') IS NOT NULL
    DROP PROCEDURE dbo.ContactTypes_insert;
GO

CREATE PROCEDURE dbo.ContactTypes_insert
    @Type VARCHAR(50),
    @IsPhoneNumberType BIT,
    @IsEmailType BIT,
    @FK_CreatedUserID INT = NULL,
    @FK_UpdatedUserID INT = NULL,
    @DateCreated DATETIME,
    @DateUpdated DATETIME = NULL
AS
BEGIN
    DECLARE @Inserted TABLE (ContactTypeID INT);

    INSERT INTO ContactTypes ([Type], IsPhoneNumberType, IsEmailType, FK_CreatedUserID, FK_UpdatedUserID, DateCreated, DateUpdated)
    OUTPUT INSERTED.ContactTypeID INTO @Inserted
    VALUES (@Type, @IsPhoneNumberType, @IsEmailType, @FK_CreatedUserID, @FK_UpdatedUserID, @DateCreated, @DateUpdated);

    SELECT *
    FROM ContactTypes
    WHERE ContactTypeID = 
    (
        SELECT TOP 1 ContactTypeID
        FROM @Inserted
    );
END
GO