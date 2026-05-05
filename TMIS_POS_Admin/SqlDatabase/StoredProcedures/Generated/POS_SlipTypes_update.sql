USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_SlipTypes_update', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_SlipTypes_update;
GO

CREATE PROCEDURE dbo.POS_SlipTypes_update
    @SlipTypeID INT,
    @SlipType VARCHAR(15),
    @SlipCode VARCHAR(20),
    @Description VARCHAR(255) = NULL,
    @FK_CreatedUserID INT,
    @FK_UpdatedUserID INT = NULL,
    @DateCreated DATETIME,
    @DateUpdated DATETIME = NULL
AS
BEGIN
    UPDATE POS_SlipTypes
    SET     SlipType = @SlipType,
    SlipCode = @SlipCode,
    Description = @Description,
    FK_CreatedUserID = @FK_CreatedUserID,
    FK_UpdatedUserID = @FK_UpdatedUserID,
    DateUpdated = @DateUpdated
    WHERE SlipTypeID = @SlipTypeID;

    SELECT *
    FROM POS_SlipTypes
    WHERE SlipTypeID = @SlipTypeID;
END
GO