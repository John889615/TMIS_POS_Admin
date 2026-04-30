USE [TMIS_Development]
GO

IF OBJECT_ID('dbo.POS_DebtorProductPrices_insert', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_DebtorProductPrices_insert;
GO

CREATE PROCEDURE dbo.POS_DebtorProductPrices_insert
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
    @DateUpdated DATETIME,
    @FK_DefaultUnitID INT = NULL
AS
BEGIN
    DECLARE @Inserted TABLE (DebtorProductPriceID INT);

    INSERT INTO POS_DebtorProductPrices (FK_DebtorProductID, FK_PriceCodeID, FK_TaxID, ItemPrice, Inclusive, Vat, StartDate, EndDate, IsActive, DateCreated, DateUpdated, FK_DefaultUnitID)
    OUTPUT INSERTED.DebtorProductPriceID INTO @Inserted
    VALUES (@FK_DebtorProductID, @FK_PriceCodeID, @FK_TaxID, @ItemPrice, @Inclusive, @Vat, @StartDate, @EndDate, @IsActive, @DateCreated, @DateUpdated, @FK_DefaultUnitID);

    SELECT *
    FROM POS_DebtorProductPrices
    WHERE DebtorProductPriceID = 
    (
        SELECT TOP 1 DebtorProductPriceID
        FROM @Inserted
    );
END
GO