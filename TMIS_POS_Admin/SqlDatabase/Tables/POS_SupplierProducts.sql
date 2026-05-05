USE [TMIS_Development]
GO

IF OBJECT_ID ('POS_SupplierProducts', 'U') IS NOT NULL
	DROP TABLE POS_SupplierProducts
GO

CREATE TABLE POS_SupplierProducts
(
	SupplierProductID INT NOT NULL PRIMARY KEY IDENTITY (1, 1),
	FK_CreditorID INT NOT NULL FOREIGN KEY REFERENCES Creditors (CreditorID),
	FK_ProductID INT NOT NULL FOREIGN KEY REFERENCES POS_Products (ProductID),
	FK_DebtorID INT NOT NULL FOREIGN KEY REFERENCES Debtors (DebtorID),
	SupplierItemCode VARCHAR(255) NOT NULL,
	-- This is the UOM for ordering
    FK_BaseUnitID INT NOT NULL FOREIGN KEY REFERENCES POS_Units (UnitID),
    FK_PacUnitID INT NULL FOREIGN KEY REFERENCES POS_Units (UnitID),
    UnitsPerPack DECIMAL(18, 4) NULL,
	Quantity DECIMAL(18, 4) NOT NULL,
	TrackPackLevel BIT NOT NULL,
	-- This was the last purchase price
	LastPurchasePrice DECIMAL(18, 4) NULL,
	LastPurchaseDate DATETIME NULL,
	FK_TaxTypeID INT NOT NULL FOREIGN KEY REFERENCES POS_TaxTypes (TaxTypeID),
	LeadTimeDays INT NULL,
	IsPreferred INT NOT NULL,
	IsActive BIT NOT NULL,
	DateAdded DATETIME NOT NULL,
	DateUpdated DATETIME NOT NULL
)