USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.CreditorTypeMappings_select_single', 'P') IS NOT NULL
    DROP PROCEDURE dbo.CreditorTypeMappings_select_single;
GO

CREATE PROCEDURE dbo.CreditorTypeMappings_select_single
    @CreditorTypeMappingID INT
AS
BEGIN
    SELECT *
    FROM CreditorTypeMappings
    WHERE CreditorTypeMappingID = @CreditorTypeMappingID;
END
GO