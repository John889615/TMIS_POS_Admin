USE [TMIS_Development]
GO

IF OBJECT_ID ('POS_Products', 'U') IS NOT NULL
	DROP TABLE POS_Products
GO

CREATE TABLE POS_Products
(
	ProductID INT NOT NULL PRIMARY KEY IDENTITY (1, 1),
	ProductName VARCHAR(255) NOT NULL,
	[Description] VARCHAR(MAX) NULL,
	ItemNo VARCHAR(50) NULL,
    FK_ProductTypeID INT NOT NULL FOREIGN KEY REFERENCES POS_ProductTypes (ProductTypeID),
	-- Dui aan of ons gaan moet count hou van die stock of nie
    IsStockTracked BIT,
	-- Measuring unit (UOM)
    FK_UnitID INT NOT NULL FOREIGN KEY REFERENCES POS_Units (UnitID),

	FK_ProductCategoryID INT NULL FOREIGN KEY REFERENCES POS_ProductCategories (ProductCategoryID),

	FK_DefaultUnitID INT NOT NULL FOREIGN KEY REFERENCES POS_Units (UnitID),
	BC_ID VARCHAR(255) NULL,
	SKU VARCHAR(255) NULL,
	Barcode VARCHAR(255) NULL,
	QrCode VARCHAR(255) NULL,
	IsActive BIT NOT NULL,
	DateAdded DATETIME NOT  NULL,
	DateUpdated DATETIME NOT NULL
)