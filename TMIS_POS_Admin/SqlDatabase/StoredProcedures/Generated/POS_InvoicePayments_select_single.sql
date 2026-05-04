USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_InvoicePayments_select_single', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_InvoicePayments_select_single;
GO

CREATE PROCEDURE dbo.POS_InvoicePayments_select_single
    @InvoicePaymentID UNIQUEIDENTIFIER
AS
BEGIN
    SELECT *
    FROM POS_InvoicePayments
    WHERE InvoicePaymentID = @InvoicePaymentID;
END
GO