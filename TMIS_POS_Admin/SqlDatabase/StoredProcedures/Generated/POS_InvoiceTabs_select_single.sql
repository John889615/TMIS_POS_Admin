USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_InvoiceTabs_select_single', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_InvoiceTabs_select_single;
GO

CREATE PROCEDURE dbo.POS_InvoiceTabs_select_single
    @InvoiceTabID UNIQUEIDENTIFIER
AS
BEGIN
    SELECT *
    FROM POS_InvoiceTabs
    WHERE InvoiceTabID = @InvoiceTabID;
END
GO