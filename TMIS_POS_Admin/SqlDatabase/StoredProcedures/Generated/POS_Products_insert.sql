USE [TMIS_Development]
GO

IF OBJECT_ID('dbo.POS_Products_insert', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_Products_insert;
GO

CREATE PROCEDURE dbo.POS_Products_insert
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
    @DateUpdated DATETIME = NULL
AS
BEGIN
    DECLARE @Inserted TABLE (ProductID INT);

    INSERT INTO POS_Products (ProductName, Description, ItemNo, FK_ProductTypeID, IsStockTracked, FK_UnitID, FK_ProductCategoryID, FK_DefaultUnitID, BC_ID, SKU, Barcode, QrCode, IsActive, DateAdded, DateUpdated)
    OUTPUT INSERTED.ProductID INTO @Inserted
    VALUES (@ProductName, @Description, @ItemNo, @FK_ProductTypeID, @IsStockTracked, @FK_UnitID, @FK_ProductCategoryID, @FK_DefaultUnitID, @BC_ID, @SKU, @Barcode, @QrCode, @IsActive, @DateAdded, @DateUpdated);

    SELECT *
    FROM POS_Products
    WHERE ProductID = 
    (
        SELECT TOP 1 ProductID
        FROM @Inserted
    );
END
GO