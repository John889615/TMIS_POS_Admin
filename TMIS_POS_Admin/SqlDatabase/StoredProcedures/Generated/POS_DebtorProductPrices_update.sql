USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_DebtorProductPrices_update', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_DebtorProductPrices_update;
GO

CREATE PROCEDURE dbo.POS_DebtorProductPrices_update
    @DebtorProductPriceID INT,
    @FK_DebtorProductID INT,
    @FK_PriceCodeID INT,
    @FK_TaxID INT,
    @ItemPrice DECIMAL (18, 4),
    @Inclusive BIT,
    @Vat DECIMAL (18, 4),
    @StartDate DATETIME = NULL,
    @EndDate DATETIME = NULL,
    @IsActive BIT,
    @DateCreated DATETIME,
    @DateUpdated DATETIME = NULL,
    @FK_DefaultUnitID INT = NULL
AS
BEGIN
    UPDATE POS_DebtorProductPrices
    SET     FK_DebtorProductID = @FK_DebtorProductID,
    FK_PriceCodeID = @FK_PriceCodeID,
    FK_TaxID = @FK_TaxID,
    ItemPrice = @ItemPrice,
    Inclusive = @Inclusive,
    Vat = @Vat,
    StartDate = @StartDate,
    EndDate = @EndDate,
    IsActive = @IsActive,
    DateUpdated = @DateUpdated,
    FK_DefaultUnitID = @FK_DefaultUnitID
    WHERE DebtorProductPriceID = @DebtorProductPriceID;

    SELECT *
    FROM POS_DebtorProductPrices
    WHERE DebtorProductPriceID = @DebtorProductPriceID;
END
GO