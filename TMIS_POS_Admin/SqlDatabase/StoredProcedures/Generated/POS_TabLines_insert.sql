USE [TMIS_Development]
GO

IF OBJECT_ID('dbo.POS_TabLines_insert', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_TabLines_insert;
GO

CREATE PROCEDURE dbo.POS_TabLines_insert
    @TabLineID UNIQUEIDENTIFIER = NULL,
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
    @DiscountPerc DECIMAL (18, 4) = NULL,
    @IsVoided BIT,
    @Notes VARCHAR(MAX) = NULL,
    @AutoNotes VARCHAR(MAX) = NULL,
    @CreatedBy VARCHAR(255),
    @DateCreated DATETIME,
    @DateUpdated DATETIME = NULL,
    @ServedAs VARCHAR(50) = NULL,
    @ServedAsQuantified BIT = NULL,
    @ServedAsQuantity DECIMAL (18, 4) = NULL,
    @FK_MenuID INT = NULL,
    @MenuName VARCHAR(100) = NULL,
    @Gratuity DECIMAL (18, 4) = NULL,
    @GratuityPerc DECIMAL (18, 4) = NULL,
    @FK_CostCenterID INT = NULL
AS
BEGIN
    DECLARE @Inserted TABLE (TabLineID UNIQUEIDENTIFIER);

    INSERT INTO POS_TabLines (TabLineID, FK_TabID, FK_ProductID, FK_PriceCodeID, FK_PointerID, UnitCostExcl, Vat, UnitCostIncl, Product, Quantity, Discount, DiscountPerc, IsVoided, Notes, AutoNotes, CreatedBy, DateCreated, DateUpdated, ServedAs, ServedAsQuantified, ServedAsQuantity, FK_MenuID, MenuName, Gratuity, GratuityPerc, FK_CostCenterID)
    OUTPUT INSERTED.TabLineID INTO @Inserted
    VALUES (ISNULL(@TabLineID, NEWID()), @FK_TabID, @FK_ProductID, @FK_PriceCodeID, @FK_PointerID, @UnitCostExcl, @Vat, @UnitCostIncl, @Product, @Quantity, @Discount, @DiscountPerc, @IsVoided, @Notes, @AutoNotes, @CreatedBy, @DateCreated, @DateUpdated, @ServedAs, @ServedAsQuantified, @ServedAsQuantity, @FK_MenuID, @MenuName, @Gratuity, @GratuityPerc, @FK_CostCenterID);

    SELECT *
    FROM POS_TabLines
    WHERE TabLineID = 
    (
        SELECT TOP 1 TabLineID
        FROM @Inserted
    );
END
GO