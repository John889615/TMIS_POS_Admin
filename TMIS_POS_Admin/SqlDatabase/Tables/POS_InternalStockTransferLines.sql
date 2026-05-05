USE [TMIS_Development]
GO

IF OBJECT_ID('POS_InternalStockTransferLines', 'U') IS NOT NULL
	DROP TABLE POS_InternalStockTransferLines
GO

CREATE TABLE POS_InternalStockTransferLines
(
	InternalStockTransferLineID INT NOT NULL PRIMARY KEY IDENTITY (1, 1),
	FK_InternalStockTransferID INT NOT NULL FOREIGN KEY REFERENCES POS_InternalStockTransfers(InternalStockTransferID),
	FK_ProductID INT NOT NULL FOREIGN KEY REFERENCES POS_Products(ProductID),
	Quantity DECIMAL(18, 4) NOT NULL
)