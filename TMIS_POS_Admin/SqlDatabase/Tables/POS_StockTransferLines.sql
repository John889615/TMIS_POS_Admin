USE [TMIS_Development]
GO

IF OBJECT_ID('POS_StockTransferLines', 'U') IS NOT NULL
	DROP TABLE POS_StockTransferLines
GO

CREATE TABLE POS_StockTransferLines
(
	StockTransferLineID INT NOT NULL PRIMARY KEY IDENTITY (1, 1),
	FK_StockTransferID INT NOT NULL FOREIGN KEY REFERENCES POS_StockTransfers(StockTransferID),
	FK_ProductID INT NOT NULL FOREIGN KEY REFERENCES POS_Products(ProductID),
	Quantity DECIMAL(18, 4) NOT NULL
)