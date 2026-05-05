USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.ContactTypes_update', 'P') IS NOT NULL
    DROP PROCEDURE dbo.ContactTypes_update;
GO

CREATE PROCEDURE dbo.ContactTypes_update
    @ContactTypeID INT,
    @Type VARCHAR(50),
    @IsPhoneNumberType BIT,
    @IsEmailType BIT
AS
BEGIN
    UPDATE ContactTypes
    SET     [Type] = @Type,
    IsPhoneNumberType = @IsPhoneNumberType,
    IsEmailType = @IsEmailType
    WHERE ContactTypeID = @ContactTypeID;

    SELECT *
    FROM ContactTypes
    WHERE ContactTypeID = @ContactTypeID;
END
GO