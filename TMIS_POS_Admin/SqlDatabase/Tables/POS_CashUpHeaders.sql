USE [TMIS_Development]
GO

IF OBJECT_ID('POS_CashUpHeaders', 'U') IS NOT NULL
	DROP TABLE POS_CashUpHeaders
GO

CREATE TABLE POS_CashUpHeaders
(
    CashUpHeaderID UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
    FK_CostCenterID INT NOT NULL FOREIGN KEY REFERENCES POS_Costcenters(CostCenterID),
    FK_CurrencyID INT NOT NULL FOREIGN KEY REFERENCES Currencies(CurrencyID),
    CashUpDate DATE NOT NULL,
    CashUpBy VARCHAR(255) NULL,
    TotalSystemAmount DECIMAL(18, 4) NULL,
    TotalCountedAmount DECIMAL(18, 4) NULL,
    TotalVariance DECIMAL(18, 4) NULL,
    Notes VARCHAR(MAX) NULL,
    IsFinalised BIT NOT NULL DEFAULT 0,
    DateCreated DATETIME NOT NULL DEFAULT GETDATE(),
    DateUpdated DATETIME NOT NULL DEFAULT GETDATE()

	CONSTRAINT cashUpHeaderBS UNIQUE (FK_CostCenterID, FK_CurrencyID, CashUpDate)
);