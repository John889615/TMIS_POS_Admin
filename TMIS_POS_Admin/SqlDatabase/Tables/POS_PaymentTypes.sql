USE [TMIS_Development]
GO

IF OBJECT_ID ('POS_PaymentTypes', 'U') IS NOT NULL
	DROP TABLE POS_PaymentTypes
GO

CREATE TABLE POS_PaymentTypes
(
	PaymentTypeID INT NOT NULL PRIMARY KEY IDENTITY (1, 1),
	FK_PaymentTypeIcon INT NOT NULL FOREIGN KEY REFERENCES POS_PaymentTypeIcons (PaymentTypeIconID),
	[Name] VARCHAR(255) NOT NULL,
	IsActive BIT NOT NULL,
	IsPrimary BIT NOT NULL,
	IsSecondary BIT NOT NULL,
	SettlePayment BIT NOT NULL,
	RequireAdditionalInfo BIT NOT NULL,
	RequireElevation BIT NOT NULL,
	DateCreated DATETIME NOT  NULL,
	DateUpdated DATETIME NOT NULL
)