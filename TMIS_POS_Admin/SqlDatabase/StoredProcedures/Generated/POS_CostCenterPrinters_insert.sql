USE [TMIS_Development]
GO

IF OBJECT_ID('dbo.POS_CostCenterPrinters_insert', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_CostCenterPrinters_insert;
GO

CREATE PROCEDURE dbo.POS_CostCenterPrinters_insert
    @FK_CostCenterID INT = NULL,
    @FK_PrinterID INT,
    @FK_CreatedUserID INT,
    @FK_UpdatedUserID INT = NULL,
    @DateCreated DATETIME,
    @DateUpdated DATETIME,
    @FK_InvoiceSlipTypeID INT = NULL,
    @FK_TabSlipTypeID INT = NULL
AS
BEGIN
    DECLARE @Inserted TABLE (CostCenterPrinterID INT);

    INSERT INTO POS_CostCenterPrinters (FK_CostCenterID, FK_PrinterID, FK_CreatedUserID, FK_UpdatedUserID, DateCreated, DateUpdated, FK_InvoiceSlipTypeID, FK_TabSlipTypeID)
    OUTPUT INSERTED.CostCenterPrinterID INTO @Inserted
    VALUES (@FK_CostCenterID, @FK_PrinterID, @FK_CreatedUserID, @FK_UpdatedUserID, @DateCreated, @DateUpdated, @FK_InvoiceSlipTypeID, @FK_TabSlipTypeID);

    SELECT *
    FROM POS_CostCenterPrinters
    WHERE CostCenterPrinterID = 
    (
        SELECT TOP 1 CostCenterPrinterID
        FROM @Inserted
    );
END
GO