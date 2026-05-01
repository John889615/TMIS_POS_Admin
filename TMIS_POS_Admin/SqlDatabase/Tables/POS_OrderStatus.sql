USE [TMIS_Development]
GO

IF OBJECT_ID('POS_OrderStatus', 'U') IS NOT NULL
	DROP TABLE POS_OrderStatus
GO

CREATE TABLE POS_OrderStatus
(
	OrderStatusID INT NOT NULL PRIMARY KEY IDENTITY (1, 1),
	OrderStatus VARCHAR(50) NOT NULL
)

INSERT INTO POS_OrderStatus(OrderStatus)
VALUES('Pending'),
	('Approved'),
	('Received'),
	('Cancelled'),
	('Draft'),
	('PartiallyApproved')
