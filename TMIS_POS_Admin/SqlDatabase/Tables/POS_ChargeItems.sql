USE [TMIS_Development]
GO

IF OBJECT_ID('dbo.POS_ChargeItems', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.POS_ChargeItems
    (
        ChargeItemID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,

        ChargeName VARCHAR(250) NOT NULL,
        [Description] VARCHAR(500) NULL,
        ItemNo VARCHAR(50) NULL,
        [Location] VARCHAR(50) NULL,

        FK_UnitID INT NULL,
        BC_ID UNIQUEIDENTIFIER NULL,

        IsActive BIT NOT NULL CONSTRAINT DF_POS_ChargeItems_IsActive DEFAULT (1),
        DateAdded DATETIME NOT NULL CONSTRAINT DF_POS_ChargeItems_DateAdded DEFAULT (GETDATE()),
        DateUpdated DATETIME NOT NULL CONSTRAINT DF_POS_ChargeItems_DateUpdated DEFAULT (GETDATE())
    );
END
GO