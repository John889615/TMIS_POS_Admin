USE [TMIS_Development]
GO

IF OBJECT_ID('POS_TabLines', 'U') IS NOT NULL
	DROP TABLE POS_TabLines
GO

CREATE TABLE POS_TabLines
(
	TabLineID UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,

	FK_TabID UNIQUEIDENTIFIER NOT NULL FOREIGN KEY REFERENCES POS_Tabs (TabID),
	FK_ProductID INT NOT NULL FOREIGN KEY REFERENCES POS_Products(ProductID),
	FK_PriceCodeID INT NOT NULL FOREIGN KEY REFERENCES POS_PriceCodes (PriceCodeID),

	FK_PointerID UNIQUEIDENTIFIER NULL,

	UnitCostExcl DECIMAL (18, 4) NOT NULL,
	Vat DECIMAL(18, 4) NOT NULL,
	UnitCostIncl DECIMAL (18, 4) NOT NULL,

	[Product] VARCHAR(50) NOT NULL,
	Quantity DECIMAL (18, 4) NOT NULL,
	Discount DECIMAL (18, 4) NULL,
	-- Spec 1: widened from INT to DECIMAL(18,4).
	DiscountPerc DECIMAL(18, 4) NULL,
	IsVoided BIT NOT NULL,
	Notes VARCHAR(MAX) NULL,
	AutoNotes VARCHAR(MAX) NULL,

	-- Per Spec 1 staff-not-synced rule: varchar shadow populated by the
	-- FOH-side translator from Staff.StaffName + ' ' + Staff.LastName.
	CreatedBy VARCHAR(255) NOT NULL,

	-- Spec 1 additions:
	ServedAs VARCHAR(50) NULL,
	ServedAsQuantified BIT NULL,
	ServedAsQuantity DECIMAL(18, 4) NULL,

	-- Spec 1 + addendum: FK references POS_DebtorMenus on Admin
	-- (FOH's MenuID corresponds to Admin's DebtorMenuID).
	FK_MenuID INT NULL FOREIGN KEY REFERENCES POS_DebtorMenus (DebtorMenuID),
	MenuName VARCHAR(100) NULL,

	-- Spec 1: gratuity carried per line.
	Gratuity DECIMAL(18, 4) NULL,
	GratuityPerc DECIMAL(18, 4) NULL,

	-- 2026-05-08: per-line cost centre, mirrors FOH TabLines.FK_CostCenterID.
	FK_CostCenterID INT NULL FOREIGN KEY REFERENCES POS_CostCenters (CostCenterID),

	DateCreated DATETIME NOT NULL,
	DateUpdated DATETIME NOT NULL
)
GO

CREATE INDEX IX_POS_TabLines_FK_CostCenterID
    ON POS_TabLines (FK_CostCenterID)
