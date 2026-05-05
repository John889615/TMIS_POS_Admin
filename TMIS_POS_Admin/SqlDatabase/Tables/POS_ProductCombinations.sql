USE [TMIS_Development]
GO

IF OBJECT_ID ('POS_ProductCombinations', 'U') IS NOT NULL
	DROP TABLE POS_ProductCombinations
GO

CREATE TABLE POS_ProductCombinations
(
	ProductCombinationID INT NOT NULL PRIMARY KEY IDENTITY (1, 1),

	FK_ProductID INT NOT NULL FOREIGN KEY REFERENCES POS_Products (ProductID),
	FK_ProductItemID INT NOT NULL FOREIGN KEY REFERENCES POS_Products (ProductID),
	IsQuantified BIT NOT NULL,
	Quantity DECIMAL(18, 4) NOT NULL,
	IsOptional BIT NOT NULL,
	IsExtraCharge BIT NOT NULL,
	[DisplayOrder] INT NULL,

	FK_CreatedUserID INT NOT NULL FOREIGN KEY REFERENCES Users (UserID),
	FK_UpdatedUserID INT NULL FOREIGN KEY REFERENCES Users (UserID),
	DateCreated DATETIME NOT NULL DEFAULT (GETDATE()),
	DateUpdated DATETIME NOT NULL DEFAULT (GETDATE()),

	CONSTRAINT unq_product_combination UNIQUE (FK_ProductID, FK_ProductItemID),
)
