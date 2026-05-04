USE [TMIS_Development]
GO

IF OBJECT_ID ('POS_ProductExtraCategories', 'U') IS NOT NULL
	DROP TABLE POS_ProductExtraCategories
GO

CREATE TABLE POS_ProductExtraCategories
(
	ProductExtraCategoryID INT NOT NULL PRIMARY KEY IDENTITY (1, 1),

	Category VARCHAR(50) NOT NULL,
	[DisplayOrder] INT NULL,
	
	FK_CreatedUserID INT NOT NULL FOREIGN KEY REFERENCES Users (UserID),
	FK_UpdatedUserID INT NULL FOREIGN KEY REFERENCES Users (UserID),
	DateCreated DATETIME NOT NULL DEFAULT (GETDATE()),
	DateUpdated DATETIME NOT NULL DEFAULT (GETDATE()),

	CONSTRAINT unq_product_extra_category UNIQUE (Category),
)