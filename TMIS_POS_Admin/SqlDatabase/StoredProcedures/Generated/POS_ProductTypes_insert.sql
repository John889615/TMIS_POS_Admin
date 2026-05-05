USE [TMIS_Development]
GO

IF OBJECT_ID('dbo.POS_ProductTypes_insert', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_ProductTypes_insert;
GO

CREATE PROCEDURE dbo.POS_ProductTypes_insert
    @ProductType VARCHAR(50),
    @IsInventory BIT,
    @IsManufactured BIT,
    @IsService BIT,
    @IsComposite BIT
AS
BEGIN
    DECLARE @Inserted TABLE (ProductTypeID INT);

    INSERT INTO POS_ProductTypes (ProductType, IsInventory, IsManufactured, IsService, IsComposite)
    OUTPUT INSERTED.ProductTypeID INTO @Inserted
    VALUES (@ProductType, @IsInventory, @IsManufactured, @IsService, @IsComposite);

    SELECT *
    FROM POS_ProductTypes
    WHERE ProductTypeID = 
    (
        SELECT TOP 1 ProductTypeID
        FROM @Inserted
    );
END
GO