USE [TMIS_Development]
GO

IF OBJECT_ID('POS_TabLinePreparationMethods', 'U') IS NOT NULL
	DROP TABLE POS_TabLinePreparationMethods
GO

CREATE TABLE POS_TabLinePreparationMethods
(
	TabLinePreparationMethodID UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,

	FK_TabLineCombinationID UNIQUEIDENTIFIER NOT NULL FOREIGN KEY REFERENCES POS_TabLineCombinations (TabLineCombinationID),
	FK_PreparationMethodID INT NOT NULL FOREIGN KEY REFERENCES POS_ProductPreparationMethods (ProductPreparationMethodID),

	PreparationMethodName VARCHAR(255) NOT NULL
)