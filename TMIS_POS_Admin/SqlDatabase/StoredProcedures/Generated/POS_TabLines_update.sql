USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_TabLines_update', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_TabLines_update;
GO

CREATE PROCEDURE dbo.POS_TabLines_update
    @TabLineID UNIQUEIDENTIFIER,
    @FK_TabID UNIQUEIDENTIFIER,
    @FK_ProductID INT,
    @FK_PriceCodeID INT,
    @FK_PointerID UNIQUEIDENTIFIER = NULL,
    @UnitCostExcl DECIMAL (18, 4),
    @Vat DECIMAL (18, 4),
    @UnitCostIncl DECIMAL (18, 4),
    @Product VARCHAR(50),
    @Quantity DECIMAL (18, 4),
    @Discount DECIMAL (18, 4) = NULL,
    @DiscountPerc INT = NULL,
    @IsVoided BIT,
    @Notes VARCHAR(MAX) = NULL,
    @AutoNotes VARCHAR(MAX) = NULL,
    @CreatedBy VARCHAR(255),
    @DateCreated DATETIME,
    @DateUpdated DATETIME,
    @ServedAs VARCHAR(50) = NULL,
    @ServedAsQuantified BIT = NULL,
    @ServedAsQuantity DECIMAL (18, 4) = NULL,
    @FK_MenuID INT = NULL,
    @MenuName VARCHAR(100) = NULL
AS
BEGIN
    UPDATE POS_TabLines
    SET     FK_TabID = @FK_TabID,
    FK_ProductID = @FK_ProductID,
    FK_PriceCodeID = @FK_PriceCodeID,
    FK_PointerID = @FK_PointerID,
    UnitCostExcl = @UnitCostExcl,
    Vat = @Vat,
    UnitCostIncl = @UnitCostIncl,
    Product = @Product,
    Quantity = @Quantity,
    Discount = @Discount,
    DiscountPerc = @DiscountPerc,
    IsVoided = @IsVoided,
    Notes = @Notes,
    AutoNotes = @AutoNotes,
    CreatedBy = @CreatedBy,
    DateUpdated = @DateUpdated,
    ServedAs = @ServedAs,
    ServedAsQuantified = @ServedAsQuantified,
    ServedAsQuantity = @ServedAsQuantity,
    FK_MenuID = @FK_MenuID,
    MenuName = @MenuName
    WHERE TabLineID = @TabLineID;

    SELECT *
    FROM POS_TabLines
    WHERE TabLineID = @TabLineID;
END
GO