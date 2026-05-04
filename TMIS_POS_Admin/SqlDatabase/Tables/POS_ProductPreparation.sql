USE [TMIS_Development]
GO

IF OBJECT_ID ('POS_ProductPreparation', 'U') IS NOT NULL
	DROP TABLE POS_ProductPreparation
GO

CREATE TABLE POS_ProductPreparation
(
	ProductPreparationID INT NOT NULL PRIMARY KEY IDENTITY (1, 1),

	FK_ProductID INT NOT NULL FOREIGN KEY REFERENCES POS_Products (ProductID),
	FK_ProductPreparationMethodID INT NOT NULL FOREIGN KEY REFERENCES POS_ProductPreparationMethods (ProductPreparationMethodID),

	FK_CreatedUserID INT NOT NULL FOREIGN KEY REFERENCES Users (UserID),
	FK_UpdatedUserID INT NULL FOREIGN KEY REFERENCES Users (UserID),
	DateCreated DATETIME NOT NULL DEFAULT (GETDATE()),
	DateUpdated DATETIME NOT NULL DEFAULT (GETDATE()),

	CONSTRAINT unq_product_preparation UNIQUE (FK_ProductID, FK_ProductPreparationMethodID),
)