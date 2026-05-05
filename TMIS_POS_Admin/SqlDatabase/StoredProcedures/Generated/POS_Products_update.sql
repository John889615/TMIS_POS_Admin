USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_Products_update', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_Products_update;
GO

CREATE PROCEDURE dbo.POS_Products_update
    @ProductID INT,
    @ProductName VARCHAR(255),
    @Description VARCHAR(MAX) = NULL,
    @ItemNo VARCHAR(50) = NULL,
    @FK_ProductTypeID INT,
    @IsStockTracked BIT = NULL,
    @FK_UnitID INT,
    @FK_ProductCategoryID INT = NULL,
    @FK_DefaultUnitID INT,
    @BC_ID VARCHAR(255) = NULL,
    @SKU VARCHAR(255) = NULL,
    @Barcode VARCHAR(255) = NULL,
    @QrCode VARCHAR(255) = NULL,
    @IsActive BIT,
    @DateAdded DATETIME,
    @DateUpdated DATETIME
AS
BEGIN
    UPDATE POS_Products
    SET     ProductName = @ProductName,
    Description = @Description,
    ItemNo = @ItemNo,
    FK_ProductTypeID = @FK_ProductTypeID,
    IsStockTracked = @IsStockTracked,
    FK_UnitID = @FK_UnitID,
    FK_ProductCategoryID = @FK_ProductCategoryID,
    FK_DefaultUnitID = @FK_DefaultUnitID,
    BC_ID = @BC_ID,
    SKU = @SKU,
    Barcode = @Barcode,
    QrCode = @QrCode,
    IsActive = @IsActive,
    DateAdded = @DateAdded,
    DateUpdated = @DateUpdated
    WHERE ProductID = @ProductID;

    SELECT *
    FROM POS_Products
    WHERE ProductID = @ProductID;
END
GO