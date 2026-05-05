USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_ProductTypes_update', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_ProductTypes_update;
GO

CREATE PROCEDURE dbo.POS_ProductTypes_update
    @ProductTypeID INT,
    @ProductType VARCHAR(50),
    @IsInventory BIT,
    @IsManufactured BIT,
    @IsService BIT,
    @IsComposite BIT
AS
BEGIN
    UPDATE POS_ProductTypes
    SET     ProductType = @ProductType,
    IsInventory = @IsInventory,
    IsManufactured = @IsManufactured,
    IsService = @IsService,
    IsComposite = @IsComposite
    WHERE ProductTypeID = @ProductTypeID;

    SELECT *
    FROM POS_ProductTypes
    WHERE ProductTypeID = @ProductTypeID;
END
GO