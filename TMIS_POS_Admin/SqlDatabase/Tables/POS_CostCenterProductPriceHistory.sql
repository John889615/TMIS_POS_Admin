USE [TMIS_Development]
GO

IF OBJECT_ID ('POS_CostCenterProductPriceHistory', 'U') IS NOT NULL
	DROP TABLE POS_CostCenterProductPriceHistory
GO

CREATE TABLE POS_CostCenterProductPriceHistory
(
	CostcenterProductPriceHistoryID INT NOT NULL PRIMARY KEY IDENTITY (1, 1),
	FK_CostCenterProductID INT NOT NULL FOREIGN KEY REFERENCES POS_CostCenterProducts (CostCenterProductID),

	[Value] DECIMAL(18, 4) NOT NULL,
	Vat DECIMAL(18, 4) NOT NULL,
	ItemPrice DECIMAL(18, 4) NOT NULL,

	ValidFrom DATETIME NOT NULL,
	ValidTo DATETIME NULL,

	FK_CreatedUserID INT NOT NULL FOREIGN KEY REFERENCES Users (UserID),
	FK_UpdatedUserID INT NULL FOREIGN KEY REFERENCES Users (UserID),
	DateCreated DATETIME NOT NULL DEFAULT (GETDATE()),
	DateUpdated DATETIME NULL DEFAULT (GETDATE())
)