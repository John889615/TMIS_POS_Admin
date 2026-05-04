USE [TMIS_Development]
GO

IF OBJECT_ID ('POS_ProductCategories', 'U') IS NOT NULL
	DROP TABLE POS_ProductCategories
GO

CREATE TABLE POS_ProductCategories
(
	ProductCategoryID INT NOT NULL PRIMARY KEY IDENTITY (1, 1),
	CategoryName VARCHAR(255) NOT NULL,
	FK_ProductCategoryID INT NULL FOREIGN KEY REFERENCES POS_ProductCategories (ProductCategoryID),
	BC_ID VARCHAR(255) NULL,
	IsMaster BIT NOT NULL,
	IsActive BIT NOT NULL,
	DateAdded DATETIME NOT  NULL,
	DateUpdated DATETIME NOT NULL
)