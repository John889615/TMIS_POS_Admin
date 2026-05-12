USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_ProductCombinations_update', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_ProductCombinations_update;
GO

CREATE PROCEDURE dbo.POS_ProductCombinations_update
    @ProductCombinationID INT,
    @FK_ProductID INT,
    @FK_ProductItemID INT,
    @IsQuantified BIT,
    @Quantity DECIMAL (18, 4),
    @IsOptional BIT,
    @IsExtraCharge BIT,
    @DisplayOrder INT = NULL,
    @FK_CreatedUserID INT,
    @FK_UpdatedUserID INT = NULL,
    @DateCreated DATETIME,
    @DateUpdated DATETIME = NULL
AS
BEGIN
    UPDATE POS_ProductCombinations
    SET     FK_ProductID = @FK_ProductID,
    FK_ProductItemID = @FK_ProductItemID,
    IsQuantified = @IsQuantified,
    Quantity = @Quantity,
    IsOptional = @IsOptional,
    IsExtraCharge = @IsExtraCharge,
    DisplayOrder = @DisplayOrder,
    FK_CreatedUserID = @FK_CreatedUserID,
    FK_UpdatedUserID = @FK_UpdatedUserID,
    DateUpdated = @DateUpdated
    WHERE ProductCombinationID = @ProductCombinationID;

    SELECT *
    FROM POS_ProductCombinations
    WHERE ProductCombinationID = @ProductCombinationID;
END
GO