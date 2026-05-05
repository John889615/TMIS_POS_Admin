USE [TMIS_Development]
GO

IF OBJECT_ID ('POS_ProductPreparationMethods', 'U') IS NOT NULL
	DROP TABLE POS_ProductPreparationMethods
GO

CREATE TABLE POS_ProductPreparationMethods
(
	ProductPreparationMethodID INT NOT NULL PRIMARY KEY IDENTITY (1, 1),

	ShortCode VARCHAR(10) NOT NULL,
	Method VARCHAR(50) NOT NULL,

	FK_CreatedUserID INT NOT NULL FOREIGN KEY REFERENCES Users (UserID),
	FK_UpdatedUserID INT NULL FOREIGN KEY REFERENCES Users (UserID),
	DateCreated DATETIME NOT NULL DEFAULT (GETDATE()),
	DateUpdated DATETIME NOT NULL DEFAULT (GETDATE()),

	CONSTRAINT unq_product_preparation_short_code UNIQUE (ShortCode),
	CONSTRAINT unq_product_preparation_method UNIQUE (Method)
)