USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_ProductSubstitutions_select_single', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_ProductSubstitutions_select_single;
GO

CREATE PROCEDURE dbo.POS_ProductSubstitutions_select_single
    @ProductSubstitutionID INT
AS
BEGIN
    SELECT *
    FROM POS_ProductSubstitutions
    WHERE ProductSubstitutionID = @ProductSubstitutionID;
END
GO