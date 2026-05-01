USE [TMIS_Development]
GO

IF OBJECT_ID('POS_StockRequestLines', 'U') IS NOT NULL
	DROP TABLE POS_StockRequestLines
GO

CREATE TABLE POS_StockRequestLines
(
	StockRequestLineID INT NOT NULL PRIMARY KEY IDENTITY (1, 1),
	FK_StockRequestID INT NOT NULL FOREIGN KEY REFERENCES POS_StockRequests(StockRequestID),
	FK_ProductID INT NOT NULL FOREIGN KEY REFERENCES POS_Products(ProductID),
	Quantity DECIMAL(18, 4) NOT NULL,
	Notes VARCHAR(255) NULL,
	ManagerNotes VARCHAR(255) NULL,
	IsDeclined BIT NOT NULL,
	ApprovedQuantity DECIMAL(18, 4) NULL
)
