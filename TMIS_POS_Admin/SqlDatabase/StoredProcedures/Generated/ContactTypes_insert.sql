USE [TMIS_Development]
GO

IF OBJECT_ID('dbo.ContactTypes_insert', 'P') IS NOT NULL
    DROP PROCEDURE dbo.ContactTypes_insert;
GO

CREATE PROCEDURE dbo.ContactTypes_insert
    @Type VARCHAR(50),
    @IsPhoneNumberType BIT,
    @IsEmailType BIT
AS
BEGIN
    DECLARE @Inserted TABLE (ContactTypeID INT);

    INSERT INTO ContactTypes ([Type], IsPhoneNumberType, IsEmailType)
    OUTPUT INSERTED.ContactTypeID INTO @Inserted
    VALUES (@Type, @IsPhoneNumberType, @IsEmailType);

    SELECT *
    FROM ContactTypes
    WHERE ContactTypeID = 
    (
        SELECT TOP 1 ContactTypeID
        FROM @Inserted
    );
END
GO