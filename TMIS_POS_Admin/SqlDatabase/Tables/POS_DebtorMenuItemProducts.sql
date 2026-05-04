USE [TMIS_Development]
GO

IF OBJECT_ID('POS_DebtorMenuItemProducts', 'U') IS NOT NULL
	DROP TABLE POS_DebtorMenuItemProducts
GO

-- we need to link menu items to when they are available, need to configure a scheduling table for these items, whether seasonal or breakfast until 11:00 am
CREATE TABLE POS_DebtorMenuItemProducts
(
	MenuItemProductID INT NOT NULL PRIMARY KEY IDENTITY (1, 1),
	FK_DebtorMenuItemID INT NULL FOREIGN KEY REFERENCES POS_DebtorMenuItems (DebtorMenuItemID),
	FK_ProductID INT NOT NULL FOREIGN KEY REFERENCES POS_Products (ProductID),
	DisplayOrder INT NOT NULL DEFAULT 0,
	IsActive BIT NOT NULL,
	DateCreated DATETIME NOT NULL,
	FK_CreatedUserID INT NOT NULL FOREIGN KEY REFERENCES Users (UserID),
	DateUpdated DATETIME NOT NULL,
	FK_UpdatedUserID INT NOT NULL FOREIGN KEY REFERENCES Users (UserID),
)