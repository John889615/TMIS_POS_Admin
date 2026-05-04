USE [TMIS_Development]
GO

IF OBJECT_ID ('POS_ProductTypes', 'U') IS NOT NULL
	DROP TABLE POS_ProductTypes
GO

CREATE TABLE POS_ProductTypes
(
	ProductTypeID INT NOT NULL PRIMARY KEY IDENTITY (1, 1),
	ProductType VARCHAR(50) NOT NULL,
	IsInventory BIT NOT NULL,
	IsManufactured BIT NOT NULL,
	IsService BIT NOT NULL,
	IsComposite BIT NOT NULL
)

INSERT INTO POS_ProductTypes (ProductType, IsInventory, IsManufactured, IsService, IsComposite)
VALUES ('Inventory', 1, 0, 0, 0)
	, ('Manufactured', 0, 1, 0, 0)
	, ('Service', 0, 0, 1, 0)
	, ('Composite', 0, 0, 0, 1)