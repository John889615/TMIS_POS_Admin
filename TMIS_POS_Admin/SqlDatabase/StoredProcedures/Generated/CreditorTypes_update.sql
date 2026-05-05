USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.CreditorTypes_update', 'P') IS NOT NULL
    DROP PROCEDURE dbo.CreditorTypes_update;
GO

CREATE PROCEDURE dbo.CreditorTypes_update
    @CreditorTypeID INT,
    @Type VARCHAR(50),
    @Description VARCHAR(255),
    @DateCreated DATETIME,
    @DateUpdated DATETIME
AS
BEGIN
    UPDATE CreditorTypes
    SET     [Type] = @Type,
    Description = @Description,
    DateUpdated = @DateUpdated
    WHERE CreditorTypeID = @CreditorTypeID;

    SELECT *
    FROM CreditorTypes
    WHERE CreditorTypeID = @CreditorTypeID;
END
GO