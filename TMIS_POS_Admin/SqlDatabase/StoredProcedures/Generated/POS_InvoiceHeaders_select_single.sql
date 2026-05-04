USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_InvoiceHeaders_select_single', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_InvoiceHeaders_select_single;
GO

CREATE PROCEDURE dbo.POS_InvoiceHeaders_select_single
    @InvoiceHeaderID UNIQUEIDENTIFIER
AS
BEGIN
    SELECT *
    FROM POS_InvoiceHeaders
    WHERE InvoiceHeaderID = @InvoiceHeaderID;
END
GO