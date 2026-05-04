USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_TaxTypes_select_single', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_TaxTypes_select_single;
GO

CREATE PROCEDURE dbo.POS_TaxTypes_select_single
    @TaxTypeID INT
AS
BEGIN
    SELECT *
    FROM POS_TaxTypes
    WHERE TaxTypeID = @TaxTypeID;
END
GO