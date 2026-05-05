USE [TMIS_Development]
GO

IF OBJECT_ID('dbo.Creditors_insert', 'P') IS NOT NULL
    DROP PROCEDURE dbo.Creditors_insert;
GO

CREATE PROCEDURE dbo.Creditors_insert
    @ShortCode VARCHAR(8),
    @Name VARCHAR(255),
    @FK_MasterCreditorID INT = NULL,
    @IsMasterCreditor BIT,
    @DateCreated DATETIME,
    @DateUpdated DATETIME,
    @BC_ID VARCHAR(255) = NULL
AS
BEGIN
    DECLARE @Inserted TABLE (CreditorID INT);

    INSERT INTO Creditors (ShortCode, [Name], FK_MasterCreditorID, IsMasterCreditor, DateCreated, DateUpdated, BC_ID)
    OUTPUT INSERTED.CreditorID INTO @Inserted
    VALUES (@ShortCode, @Name, @FK_MasterCreditorID, @IsMasterCreditor, @DateCreated, @DateUpdated, @BC_ID);

    SELECT *
    FROM Creditors
    WHERE CreditorID = 
    (
        SELECT TOP 1 CreditorID
        FROM @Inserted
    );
END
GO