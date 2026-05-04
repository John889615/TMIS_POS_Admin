USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_InvoiceLines_select_single', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_InvoiceLines_select_single;
GO

CREATE PROCEDURE dbo.POS_InvoiceLines_select_single
    @InvoiceLineID UNIQUEIDENTIFIER
AS
BEGIN
    SELECT *
    FROM POS_InvoiceLines
    WHERE InvoiceLineID = @InvoiceLineID;
END
GO