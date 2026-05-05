USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.Creditors_update', 'P') IS NOT NULL
    DROP PROCEDURE dbo.Creditors_update;
GO

CREATE PROCEDURE dbo.Creditors_update
    @CreditorID INT,
    @ShortCode VARCHAR(8),
    @Name VARCHAR(255),
    @FK_MasterCreditorID INT = NULL,
    @IsMasterCreditor BIT,
    @DateCreated DATETIME,
    @DateUpdated DATETIME,
    @BC_ID VARCHAR(255) = NULL
AS
BEGIN
    UPDATE Creditors
    SET     ShortCode = @ShortCode,
    [Name] = @Name,
    FK_MasterCreditorID = @FK_MasterCreditorID,
    IsMasterCreditor = @IsMasterCreditor,
    DateUpdated = @DateUpdated,
    BC_ID = @BC_ID
    WHERE CreditorID = @CreditorID;

    SELECT *
    FROM Creditors
    WHERE CreditorID = @CreditorID;
END
GO