USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_DebtorProducts_update', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_DebtorProducts_update;
GO

CREATE PROCEDURE dbo.POS_DebtorProducts_update
    @DebtorProductID INT,
    @FK_ProductID INT,
    @FK_LocationID INT,
    @CostPrice DECIMAL (18, 4),
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
    UPDATE POS_DebtorProducts
    SET     FK_ProductID = @FK_ProductID,
    FK_LocationID = @FK_LocationID,
    CostPrice = @CostPrice,
    FK_SellUnitID = @FK_SellUnitID,
    QuantityOnHand = @QuantityOnHand,
    IsAvailable = @IsAvailable,
    IsActive = @IsActive,
    FK_CreatedUserID = @FK_CreatedUserID,
    FK_UpdatedUserID = @FK_UpdatedUserID,
    DateUpdated = @DateUpdated
    WHERE DebtorProductID = @DebtorProductID;

    SELECT *
    FROM POS_DebtorProducts
    WHERE DebtorProductID = @DebtorProductID;
END
GO