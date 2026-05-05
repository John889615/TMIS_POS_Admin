USE [TMIS_Development]
GO

IF OBJECT_ID('dbo.POS_SlipTypes_insert', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_SlipTypes_insert;
GO

CREATE PROCEDURE dbo.POS_SlipTypes_insert
    @SlipType VARCHAR(15),
    @SlipCode VARCHAR(20),
    @Description VARCHAR(255) = NULL,
    @FK_CreatedUserID INT,
    @FK_UpdatedUserID INT = NULL,
    @DateCreated DATETIME,
    @DateUpdated DATETIME = NULL
AS
BEGIN
    DECLARE @Inserted TABLE (SlipTypeID INT);

    INSERT INTO POS_SlipTypes (SlipType, SlipCode, Description, FK_CreatedUserID, FK_UpdatedUserID, DateCreated, DateUpdated)
    OUTPUT INSERTED.SlipTypeID INTO @Inserted
    VALUES (@SlipType, @SlipCode, @Description, @FK_CreatedUserID, @FK_UpdatedUserID, @DateCreated, @DateUpdated);

    SELECT *
    FROM POS_SlipTypes
    WHERE SlipTypeID = 
    (
        SELECT TOP 1 SlipTypeID
        FROM @Inserted
    );
END
GO