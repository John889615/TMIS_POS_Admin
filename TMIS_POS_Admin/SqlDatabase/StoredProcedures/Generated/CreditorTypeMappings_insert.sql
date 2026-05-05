USE [TMIS_Development]
GO

IF OBJECT_ID('dbo.CreditorTypeMappings_insert', 'P') IS NOT NULL
    DROP PROCEDURE dbo.CreditorTypeMappings_insert;
GO

CREATE PROCEDURE dbo.CreditorTypeMappings_insert
    @FK_CreditorID INT,
    @FK_CreditorTypeID INT,
    @FK_StatusID INT,
    @DateCreated DATETIME,
    @DateUpdated DATETIME
AS
BEGIN
    DECLARE @Inserted TABLE (CreditorTypeMappingID INT);

    INSERT INTO CreditorTypeMappings (FK_CreditorID, FK_CreditorTypeID, FK_StatusID, DateCreated, DateUpdated)
    OUTPUT INSERTED.CreditorTypeMappingID INTO @Inserted
    VALUES (@FK_CreditorID, @FK_CreditorTypeID, @FK_StatusID, @DateCreated, @DateUpdated);

    SELECT *
    FROM CreditorTypeMappings
    WHERE CreditorTypeMappingID = 
    (
        SELECT TOP 1 CreditorTypeMappingID
        FROM @Inserted
    );
END
GO