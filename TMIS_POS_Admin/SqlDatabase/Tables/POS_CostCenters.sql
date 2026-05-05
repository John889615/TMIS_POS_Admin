USE [TMIS_Development]
GO

IF OBJECT_ID('POS_CostCenters', 'U') IS NOT NULL
	DROP TABLE POS_CostCenters
GO

CREATE TABLE POS_CostCenters
(
	CostCenterID INT NOT NULL PRIMARY KEY IDENTITY (1, 1),
	FK_LocationID INT NOT NULL FOREIGN KEY REFERENCES POS_Locations (LocationID),
	[Name] VARCHAR(255) NOT NULL,
	-- What is shown at the top of the slip
	BillingReference VARCHAR(255) NOT NULL,
	FK_StatusID INT NOT NULL FOREIGN KEY REFERENCES Statuses (StatusID),
	FK_CostCenterTypeID INT NOT NULL FOREIGN KEY REFERENCES POS_CostCenterTypes (CostCenterTypeID),
	BC_ID VARCHAR(255) NULL,
	DateCreated DATETIME NOT NULL,
	DateUpdated DATETIME NOT NULL
)