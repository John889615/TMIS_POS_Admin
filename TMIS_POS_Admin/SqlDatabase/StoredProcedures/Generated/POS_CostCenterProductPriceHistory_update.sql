USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_CostCenterProductPriceHistory_update', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_CostCenterProductPriceHistory_update;
GO

CREATE PROCEDURE dbo.POS_CostCenterProductPriceHistory_update
    @CostcenterProductPriceHistoryID INT,
    @FK_CostCenterProductID INT,
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
    UPDATE POS_CostCenterProductPriceHistory
    SET     FK_CostCenterProductID = @FK_CostCenterProductID,
    [Value] = @Value,
    Vat = @Vat,
    ItemPrice = @ItemPrice,
    ValidFrom = @ValidFrom,
    ValidTo = @ValidTo,
    FK_CreatedUserID = @FK_CreatedUserID,
    FK_UpdatedUserID = @FK_UpdatedUserID,
    DateUpdated = @DateUpdated
    WHERE CostcenterProductPriceHistoryID = @CostcenterProductPriceHistoryID;

    SELECT *
    FROM POS_CostCenterProductPriceHistory
    WHERE CostcenterProductPriceHistoryID = @CostcenterProductPriceHistoryID;
END
GO