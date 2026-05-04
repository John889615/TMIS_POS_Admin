USE [TMIS_Development]
GO

IF OBJECT_ID('dbo.POS_InvoiceTabs_insert', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_InvoiceTabs_insert;
GO

CREATE PROCEDURE dbo.POS_InvoiceTabs_insert
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
    DECLARE @Inserted TABLE (InvoiceTabID UNIQUEIDENTIFIER);

    INSERT INTO POS_InvoiceTabs (FK_InvoiceHeaderID, FK_TabID, TabGratuity, TabDiscount, TabTotalExcl, TabTotalVat, TabTotalIncl, TabDateOpened, TabDateClosed)
    OUTPUT INSERTED.InvoiceTabID INTO @Inserted
    VALUES (@FK_InvoiceHeaderID, @FK_TabID, @TabGratuity, @TabDiscount, @TabTotalExcl, @TabTotalVat, @TabTotalIncl, @TabDateOpened, @TabDateClosed);

    SELECT *
    FROM POS_InvoiceTabs
    WHERE InvoiceTabID = 
    (
        SELECT TOP 1 InvoiceTabID
        FROM @Inserted
    );
END
GO