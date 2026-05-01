USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_PriceCodes_select_single', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_PriceCodes_select_single;
GO

CREATE PROCEDURE dbo.POS_PriceCodes_select_single
    @PriceCodeID INT
AS
BEGIN
    SELECT *
    FROM POS_PriceCodes
    WHERE PriceCodeID = @PriceCodeID;
END
GO