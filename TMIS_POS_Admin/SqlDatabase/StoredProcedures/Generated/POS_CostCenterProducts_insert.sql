USE [TMIS_Development]
GO

IF OBJECT_ID('dbo.POS_CostCenterProducts_insert', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_CostCenterProducts_insert;
GO

CREATE PROCEDURE dbo.POS_CostCenterProducts_insert
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
    DECLARE @Inserted TABLE (CostCenterProductID INT);

    INSERT INTO POS_CostCenterProducts (FK_ProductID, FK_CostCenterID, FK_TaxTypeID, [Value], Vat, ItemPrice, FK_SellUnitID, QuantityOnHand, IsAvailable, IsActive, FK_CreatedUserID, FK_UpdatedUserID, DateCreated, DateUpdated)
    OUTPUT INSERTED.CostCenterProductID INTO @Inserted
    VALUES (@FK_ProductID, @FK_CostCenterID, @FK_TaxTypeID, @Value, @Vat, @ItemPrice, @FK_SellUnitID, @QuantityOnHand, @IsAvailable, @IsActive, @FK_CreatedUserID, @FK_UpdatedUserID, @DateCreated, @DateUpdated);

    SELECT *
    FROM POS_CostCenterProducts
    WHERE CostCenterProductID = 
    (
        SELECT TOP 1 CostCenterProductID
        FROM @Inserted
    );
END
GO