USE [TMIS_Development]
GO

IF OBJECT_ID ('POS_ProductSubstitutions', 'U') IS NOT NULL
	DROP TABLE POS_ProductSubstitutions
GO

CREATE TABLE POS_ProductSubstitutions
(
	ProductSubstitutionID INT NOT NULL PRIMARY KEY IDENTITY (1, 1),

	FK_ProductID INT NOT NULL FOREIGN KEY REFERENCES POS_Products (ProductID),
	FK_ProductSubstitutionID INT NOT NULL FOREIGN KEY REFERENCES POS_Products (ProductID),
	
	IsQuantified BIT NOT NULL,
	Quantity DECIMAL(18, 4) NOT NULL,
	
	IsExtraCharge BIT NOT NULL,

	FK_CreatedUserID INT NOT NULL FOREIGN KEY REFERENCES Users (UserID),
	FK_UpdatedUserID INT NULL FOREIGN KEY REFERENCES Users (UserID),
	DateCreated DATETIME NOT NULL DEFAULT (GETDATE()),
	DateUpdated DATETIME NOT NULL DEFAULT (GETDATE()),

	CONSTRAINT unq_product_substitution UNIQUE (FK_ProductID, FK_ProductSubstitutionID),
)