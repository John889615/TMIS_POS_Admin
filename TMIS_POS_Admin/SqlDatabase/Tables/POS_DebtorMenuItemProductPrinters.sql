USE [TMIS_Development]
GO

IF OBJECT_ID('POS_DebtorMenuItemProductPrinters', 'U') IS NOT NULL
	DROP TABLE POS_DebtorMenuItemProductPrinters
GO

CREATE TABLE POS_DebtorMenuItemProductPrinters
(
	DebtorMenuItemProductPrinterID INT NOT NULL PRIMARY KEY IDENTITY (1, 1),
	FK_MenuItemProductID INT NULL FOREIGN KEY REFERENCES POS_DebtorMenuItemProducts (MenuItemProductID),
	FK_PrinterID INT NOT NULL FOREIGN KEY REFERENCES POS_SlipPrinters (SlipPrinterID),
	FK_CreatedUserID INT NOT NULL FOREIGN KEY REFERENCES Users (UserID),
	FK_UpdatedUserID INT NULL FOREIGN KEY REFERENCES Users (UserID),
	DateCreated DATETIME NOT NULL,
	DateUpdated DATETIME NOT NULL
)