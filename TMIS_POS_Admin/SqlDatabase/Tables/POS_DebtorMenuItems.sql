USE [TMIS_Development]
GO

IF OBJECT_ID('POS_DebtorMenuItems', 'U') IS NOT NULL
	DROP TABLE POS_DebtorMenuItems
GO

-- we need to link menu items to when they are available, need to configure a scheduling table for these items, whether seasonal or breakfast until 11:00 am
CREATE TABLE POS_DebtorMenuItems
(
	DebtorMenuItemID INT NOT NULL PRIMARY KEY IDENTITY (1, 1),
	FK_DebtorMenuID INT NULL FOREIGN KEY REFERENCES POS_DebtorMenus (DebtorMenuID),
	Item VARCHAR(50) NOT NULL,
	[Description] VARCHAR(255) NULL,
	FK_MenuItemID INT NULL FOREIGN KEY REFERENCES POS_DebtorMenuItems (DebtorMenuItemID),
	FK_ReferenceInsertID INT NULL,
	DateCreated DATETIME NOT NULL,
	FK_CreatedUserID INT NOT NULL FOREIGN KEY REFERENCES Users (UserID),
	DateUpdated DATETIME NOT NULL,
	FK_UpdatedUserID INT NOT NULL FOREIGN KEY REFERENCES Users (UserID),
)