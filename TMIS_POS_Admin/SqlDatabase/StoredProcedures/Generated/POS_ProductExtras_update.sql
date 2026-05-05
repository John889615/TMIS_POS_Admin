USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_ProductExtras_update', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_ProductExtras_update;
GO

CREATE PROCEDURE dbo.POS_ProductExtras_update
    @ProductExtraID INT,
    @FK_ProductID INT,
    @FK_ProductExtraCategoryID INT,
    @FK_ProductExtraID INT,
    @IsQuantified BIT,
    @Quantity DECIMAL (18, 4),
    @IsExtraCharge BIT,
    @DisplayOrder INT = NULL,
    @FK_CreatedUserID INT,
    @FK_UpdatedUserID INT = NULL,
    @DateCreated DATETIME,
    @DateUpdated DATETIME
AS
BEGIN
    UPDATE POS_ProductExtras
    SET     FK_ProductID = @FK_ProductID,
    FK_ProductExtraCategoryID = @FK_ProductExtraCategoryID,
    FK_ProductExtraID = @FK_ProductExtraID,
    IsQuantified = @IsQuantified,
    Quantity = @Quantity,
    IsExtraCharge = @IsExtraCharge,
    DisplayOrder = @DisplayOrder,
    FK_CreatedUserID = @FK_CreatedUserID,
    FK_UpdatedUserID = @FK_UpdatedUserID,
    DateUpdated = @DateUpdated
    WHERE ProductExtraID = @ProductExtraID;

    SELECT *
    FROM POS_ProductExtras
    WHERE ProductExtraID = @ProductExtraID;
END
GO