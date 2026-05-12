USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_CostCenterPrinters_update', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_CostCenterPrinters_update;
GO

CREATE PROCEDURE dbo.POS_CostCenterPrinters_update
    @CostCenterPrinterID INT,
    @FK_CostCenterID INT = NULL,
    @FK_PrinterID INT,
    @FK_CreatedUserID INT,
    @FK_UpdatedUserID INT = NULL,
    @DateCreated DATETIME,
    @DateUpdated DATETIME = NULL,
    @FK_InvoiceSlipTypeID INT = NULL,
    @FK_TabSlipTypeID INT = NULL
AS
BEGIN
    UPDATE POS_CostCenterPrinters
    SET     FK_CostCenterID = @FK_CostCenterID,
    FK_PrinterID = @FK_PrinterID,
    FK_CreatedUserID = @FK_CreatedUserID,
    FK_UpdatedUserID = @FK_UpdatedUserID,
    DateUpdated = @DateUpdated,
    FK_InvoiceSlipTypeID = @FK_InvoiceSlipTypeID,
    FK_TabSlipTypeID = @FK_TabSlipTypeID
    WHERE CostCenterPrinterID = @CostCenterPrinterID;

    SELECT *
    FROM POS_CostCenterPrinters
    WHERE CostCenterPrinterID = @CostCenterPrinterID;
END
GO