USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_CostCenterProducts_update', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_CostCenterProducts_update;
GO

CREATE PROCEDURE dbo.POS_CostCenterProducts_update
    @CostCenterProductID INT,
    @FK_ProductID INT,
    @FK_CostCenterID INT,
    @FK_TaxTypeID INT,
    @Value DECIMAL (18, 4),
    @Vat DECIMAL (18, 4),
    @ItemPrice DECIMAL (18, 4),
    @FK_SellUnitID INT,
    @QuantityOnHand DECIMAL (18, 4),
    @IsAvailable BIT,
    @IsActive BIT,
    @FK_CreatedUserID INT,
    @FK_UpdatedUserID INT = NULL,
    @DateCreated DATETIME,
    @DateUpdated DATETIME = NULL
AS
BEGIN
    UPDATE POS_CostCenterProducts
    SET     FK_ProductID = @FK_ProductID,
    FK_CostCenterID = @FK_CostCenterID,
    FK_TaxTypeID = @FK_TaxTypeID,
    [Value] = @Value,
    Vat = @Vat,
    ItemPrice = @ItemPrice,
    FK_SellUnitID = @FK_SellUnitID,
    QuantityOnHand = @QuantityOnHand,
    IsAvailable = @IsAvailable,
    IsActive = @IsActive,
    FK_CreatedUserID = @FK_CreatedUserID,
    FK_UpdatedUserID = @FK_UpdatedUserID,
    DateUpdated = @DateUpdated
    WHERE CostCenterProductID = @CostCenterProductID;

    SELECT *
    FROM POS_CostCenterProducts
    WHERE CostCenterProductID = @CostCenterProductID;
END
GO