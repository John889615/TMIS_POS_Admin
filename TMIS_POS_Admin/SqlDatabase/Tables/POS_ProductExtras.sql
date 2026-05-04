USE [TMIS_Development]
GO

IF OBJECT_ID ('POS_ProductExtras', 'U') IS NOT NULL
	DROP TABLE POS_ProductExtras
GO

CREATE TABLE POS_ProductExtras
(
	ProductExtraID INT NOT NULL PRIMARY KEY IDENTITY (1, 1),

	FK_ProductID INT NOT NULL FOREIGN KEY REFERENCES POS_Products (ProductID),
	FK_ProductExtraCategoryID INT NOT NULL FOREIGN KEY REFERENCES POS_ProductExtraCategories (ProductExtraCategoryID),
	FK_ProductExtraID INT NOT NULL FOREIGN KEY REFERENCES POS_Products (ProductID),
	
	IsQuantified BIT NOT NULL,
	Quantity DECIMAL(18, 4) NOT NULL,
	
	IsExtraCharge BIT NOT NULL,
	[DisplayOrder] INT NULL,

	FK_CreatedUserID INT NOT NULL FOREIGN KEY REFERENCES Users (UserID),
	FK_UpdatedUserID INT NULL FOREIGN KEY REFERENCES Users (UserID),
	DateCreated DATETIME NOT NULL DEFAULT (GETDATE()),
	DateUpdated DATETIME NOT NULL DEFAULT (GETDATE()),

	CONSTRAINT unq_product_extra UNIQUE (FK_ProductID, FK_ProductExtraID),
)