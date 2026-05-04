USE [TMIS_Development]
GO

IF OBJECT_ID('dbo.CreditorTypes_insert', 'P') IS NOT NULL
    DROP PROCEDURE dbo.CreditorTypes_insert;
GO

CREATE PROCEDURE dbo.CreditorTypes_insert
    @Type VARCHAR(50),
    @Description VARCHAR(255),
    @DateCreated DATETIME,
    @DateUpdated DATETIME
AS
BEGIN
    DECLARE @Inserted TABLE (CreditorTypeID INT);

    INSERT INTO CreditorTypes ([Type], Description, DateCreated, DateUpdated)
    OUTPUT INSERTED.CreditorTypeID INTO @Inserted
    VALUES (@Type, @Description, @DateCreated, @DateUpdated);

    SELECT *
    FROM CreditorTypes
    WHERE CreditorTypeID = 
    (
        SELECT TOP 1 CreditorTypeID
        FROM @Inserted
    );
END
GO