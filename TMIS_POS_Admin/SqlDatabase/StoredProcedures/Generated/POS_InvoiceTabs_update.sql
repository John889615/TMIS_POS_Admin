USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_InvoiceTabs_update', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_InvoiceTabs_update;
GO

CREATE PROCEDURE dbo.POS_InvoiceTabs_update
    @InvoiceTabID UNIQUEIDENTIFIER,
    @FK_InvoiceHeaderID UNIQUEIDENTIFIER,
    @FK_TabID UNIQUEIDENTIFIER,
    @TabGratuity DECIMAL (18, 4),
    @TabDiscount DECIMAL (18, 4),
    @TabTotalExcl DECIMAL (18, 4),
    @TabTotalVat DECIMAL (18, 4),
    @TabTotalIncl DECIMAL (18, 4),
    @TabDateOpened DATETIME,
    @TabDateClosed DATETIME
AS
BEGIN
    UPDATE POS_InvoiceTabs
    SET     FK_InvoiceHeaderID = @FK_InvoiceHeaderID,
    FK_TabID = @FK_TabID,
    TabGratuity = @TabGratuity,
    TabDiscount = @TabDiscount,
    TabTotalExcl = @TabTotalExcl,
    TabTotalVat = @TabTotalVat,
    TabTotalIncl = @TabTotalIncl,
    TabDateOpened = @TabDateOpened,
    TabDateClosed = @TabDateClosed
    WHERE InvoiceTabID = @InvoiceTabID;

    SELECT *
    FROM POS_InvoiceTabs
    WHERE InvoiceTabID = @InvoiceTabID;
END
GO