USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_PriceCodes_update', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_PriceCodes_update;
GO

CREATE PROCEDURE dbo.POS_PriceCodes_update
    @PriceCodeID INT,
    @PriceCode VARCHAR(20),
    @Description VARCHAR(255) = NULL,
    @IsActive BIT,
    @DateCreated DATETIME,
    @DateUpdated DATETIME
AS
BEGIN
    UPDATE POS_PriceCodes
    SET     PriceCode = @PriceCode,
    Description = @Description,
    IsActive = @IsActive,
    DateUpdated = @DateUpdated
    WHERE PriceCodeID = @PriceCodeID;

    SELECT *
    FROM POS_PriceCodes
    WHERE PriceCodeID = @PriceCodeID;
END
GO