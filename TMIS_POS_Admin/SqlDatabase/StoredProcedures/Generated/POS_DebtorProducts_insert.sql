USE [TMIS_Development]
GO

IF OBJECT_ID('dbo.POS_DebtorProducts_insert', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_DebtorProducts_insert;
GO

CREATE PROCEDURE dbo.POS_DebtorProducts_insert
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
    DECLARE @Inserted TABLE (DebtorProductID INT);

    INSERT INTO POS_DebtorProducts (FK_ProductID, FK_LocationID, CostPrice, FK_SellUnitID, QuantityOnHand, IsAvailable, IsActive, FK_CreatedUserID, FK_UpdatedUserID, DateCreated, DateUpdated)
    OUTPUT INSERTED.DebtorProductID INTO @Inserted
    VALUES (@FK_ProductID, @FK_LocationID, @CostPrice, @FK_SellUnitID, @QuantityOnHand, @IsAvailable, @IsActive, @FK_CreatedUserID, @FK_UpdatedUserID, @DateCreated, @DateUpdated);

    SELECT *
    FROM POS_DebtorProducts
    WHERE DebtorProductID = 
    (
        SELECT TOP 1 DebtorProductID
        FROM @Inserted
    );
END
GO