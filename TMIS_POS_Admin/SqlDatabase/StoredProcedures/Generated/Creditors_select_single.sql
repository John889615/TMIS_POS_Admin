USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.Creditors_select_single', 'P') IS NOT NULL
    DROP PROCEDURE dbo.Creditors_select_single;
GO

CREATE PROCEDURE dbo.Creditors_select_single
    @CreditorID INT
AS
BEGIN
    SELECT *
    FROM Creditors
    WHERE CreditorID = @CreditorID;
END
GO