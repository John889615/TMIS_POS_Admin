USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.CreditorTypes_select_single', 'P') IS NOT NULL
    DROP PROCEDURE dbo.CreditorTypes_select_single;
GO

CREATE PROCEDURE dbo.CreditorTypes_select_single
    @CreditorTypeID INT
AS
BEGIN
    SELECT *
    FROM CreditorTypes
    WHERE CreditorTypeID = @CreditorTypeID;
END
GO