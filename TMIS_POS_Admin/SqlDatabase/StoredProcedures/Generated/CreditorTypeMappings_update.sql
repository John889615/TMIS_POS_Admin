USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.CreditorTypeMappings_update', 'P') IS NOT NULL
    DROP PROCEDURE dbo.CreditorTypeMappings_update;
GO

CREATE PROCEDURE dbo.CreditorTypeMappings_update
    @CreditorTypeMappingID INT,
    @FK_CreditorID INT,
    @FK_CreditorTypeID INT,
    @FK_StatusID INT,
    @DateCreated DATETIME,
    @DateUpdated DATETIME
AS
BEGIN
    UPDATE CreditorTypeMappings
    SET     FK_CreditorID = @FK_CreditorID,
    FK_CreditorTypeID = @FK_CreditorTypeID,
    FK_StatusID = @FK_StatusID,
    DateUpdated = @DateUpdated
    WHERE CreditorTypeMappingID = @CreditorTypeMappingID;

    SELECT *
    FROM CreditorTypeMappings
    WHERE CreditorTypeMappingID = @CreditorTypeMappingID;
END
GO