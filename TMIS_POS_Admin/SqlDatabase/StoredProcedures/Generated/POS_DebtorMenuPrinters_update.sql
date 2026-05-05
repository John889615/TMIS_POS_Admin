USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_DebtorMenuPrinters_update', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_DebtorMenuPrinters_update;
GO

CREATE PROCEDURE dbo.POS_DebtorMenuPrinters_update
    @DebtorMenuPrinterID INT,
    @FK_DebtorMenuID INT = NULL,
    @FK_PrinterID INT,
    @FK_CreatedUserID INT,
    @FK_UpdatedUserID INT = NULL,
    @DateCreated DATETIME,
    @DateUpdated DATETIME,
    @FK_OrderSlipTypeID INT = NULL
AS
BEGIN
    UPDATE POS_DebtorMenuPrinters
    SET     FK_DebtorMenuID = @FK_DebtorMenuID,
    FK_PrinterID = @FK_PrinterID,
    FK_CreatedUserID = @FK_CreatedUserID,
    FK_UpdatedUserID = @FK_UpdatedUserID,
    DateUpdated = @DateUpdated,
    FK_OrderSlipTypeID = @FK_OrderSlipTypeID
    WHERE DebtorMenuPrinterID = @DebtorMenuPrinterID;

    SELECT *
    FROM POS_DebtorMenuPrinters
    WHERE DebtorMenuPrinterID = @DebtorMenuPrinterID;
END
GO