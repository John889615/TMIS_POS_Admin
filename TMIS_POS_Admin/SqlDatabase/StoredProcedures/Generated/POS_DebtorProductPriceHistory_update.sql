USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_DebtorProductPriceHistory_update', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_DebtorProductPriceHistory_update;
GO

CREATE PROCEDURE dbo.POS_DebtorProductPriceHistory_update
    @DebtorProductPriceHistoryID INT,
    @FK_DebtorProductID INT,
    @Value DECIMAL (18, 4),
    @Vat DECIMAL (18, 4),
    @ItemPrice DECIMAL (18, 4),
    @ValidFrom DATETIME,
    @ValidTo DATETIME = NULL,
    @FK_CreatedUserID INT,
    @FK_UpdatedUserID INT = NULL,
    @DateCreated DATETIME,
    @DateUpdated DATETIME = NULL
AS
BEGIN
    UPDATE POS_DebtorProductPriceHistory
    SET     FK_DebtorProductID = @FK_DebtorProductID,
    [Value] = @Value,
    Vat = @Vat,
    ItemPrice = @ItemPrice,
    ValidFrom = @ValidFrom,
    ValidTo = @ValidTo,
    FK_CreatedUserID = @FK_CreatedUserID,
    FK_UpdatedUserID = @FK_UpdatedUserID,
    DateUpdated = @DateUpdated
    WHERE DebtorProductPriceHistoryID = @DebtorProductPriceHistoryID;

    SELECT *
    FROM POS_DebtorProductPriceHistory
    WHERE DebtorProductPriceHistoryID = @DebtorProductPriceHistoryID;
END
GO