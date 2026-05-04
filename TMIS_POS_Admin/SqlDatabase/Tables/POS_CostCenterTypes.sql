USE [TMIS_Development]
GO

IF OBJECT_ID('POS_CostCenterTypes', 'U') IS NOT NULL
	DROP TABLE POS_CostCenterTypes
GO

CREATE TABLE POS_CostCenterTypes
(
	CostCenterTypeID INT NOT NULL PRIMARY KEY IDENTITY (1, 1),
	[Name] VARCHAR(50) NOT NULL,
	DateCreated DATETIME NOT NULL,
	DateUpdated DATETIME NOT NULL
)

INSERT INTO POS_CostCenterTypes([Name], DateCreated, DateUpdated)
VALUES ('Curio Shop', GETDATE(), GETDATE()),
('Bar', GETDATE(), GETDATE()),
('Restaurant', GETDATE(), GETDATE()),
('Online Shop', GETDATE(), GETDATE()),
('Warehouse', GETDATE(), GETDATE())