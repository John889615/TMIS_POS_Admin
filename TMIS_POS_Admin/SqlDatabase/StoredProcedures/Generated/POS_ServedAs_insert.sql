USE [TMIS_Development]
GO

IF OBJECT_ID('dbo.POS_ServedAs_insert', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_ServedAs_insert;
GO

CREATE PROCEDURE dbo.POS_ServedAs_insert
    @ServedAsType VARCHAR(20),
    @Name VARCHAR(50),
    @FK_CreatedUserID INT,
    @FK_UpdatedUserID INT = NULL,
    @DateCreated DATETIME,
    @DateUpdated DATETIME = NULL
AS
BEGIN
    DECLARE @Inserted TABLE (ServedAsID INT);

    INSERT INTO POS_ServedAs (ServedAsType, [Name], FK_CreatedUserID, FK_UpdatedUserID, DateCreated, DateUpdated)
    OUTPUT INSERTED.ServedAsID INTO @Inserted
    VALUES (@ServedAsType, @Name, @FK_CreatedUserID, @FK_UpdatedUserID, @DateCreated, @DateUpdated);

    SELECT *
    FROM POS_ServedAs
    WHERE ServedAsID = 
    (
        SELECT TOP 1 ServedAsID
        FROM @Inserted
    );
END
GO