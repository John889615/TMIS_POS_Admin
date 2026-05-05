USE [TMIS_Development]
GO

IF OBJECT_ID('POS_ImageCategories', 'U') IS NOT NULL
	DROP TABLE POS_ImageCategories
GO

CREATE TABLE POS_ImageCategories
(
	ImageCategoryID INT NOT NULL PRIMARY KEY IDENTITY (1, 1),
	Category VARCHAR(50) NOT NULL,
	DateCreated DATETIME NOT NULL DEFAULT GETDATE(),
	DateUpdated DATETIME NOT NULL DEFAULT GETDATE()
)

INSERT INTO POS_ImageCategories (Category, DateCreated, DateUpdated)
VALUES ('Menu', GETDATE(), GETDATE()),
 ('MenuItem', GETDATE(), GETDATE()),
 ('Product', GETDATE(), GETDATE()),
 ('CostCenter', GETDATE(), GETDATE())