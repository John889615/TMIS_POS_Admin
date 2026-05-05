USE [TMIS_Development]
GO

IF OBJECT_ID('dbo.POS_DebtorMenuPrinters_insert', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_DebtorMenuPrinters_insert;
GO

CREATE PROCEDURE dbo.POS_DebtorMenuPrinters_insert
    @FK_DebtorMenuID INT = NULL,
    @FK_PrinterID INT,
    @FK_CreatedUserID INT,
    @FK_UpdatedUserID INT = NULL,
    @DateCreated DATETIME,
    @DateUpdated DATETIME,
    @FK_OrderSlipTypeID INT = NULL
AS
BEGIN
    DECLARE @Inserted TABLE (DebtorMenuPrinterID INT);

    INSERT INTO POS_DebtorMenuPrinters (FK_DebtorMenuID, FK_PrinterID, FK_CreatedUserID, FK_UpdatedUserID, DateCreated, DateUpdated, FK_OrderSlipTypeID)
    OUTPUT INSERTED.DebtorMenuPrinterID INTO @Inserted
    VALUES (@FK_DebtorMenuID, @FK_PrinterID, @FK_CreatedUserID, @FK_UpdatedUserID, @DateCreated, @DateUpdated, @FK_OrderSlipTypeID);

    SELECT *
    FROM POS_DebtorMenuPrinters
    WHERE DebtorMenuPrinterID = 
    (
        SELECT TOP 1 DebtorMenuPrinterID
        FROM @Inserted
    );
END
GO