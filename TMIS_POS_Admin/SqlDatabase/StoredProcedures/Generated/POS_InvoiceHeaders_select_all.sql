USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_InvoiceHeaders_select_all', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_InvoiceHeaders_select_all;
GO

CREATE PROCEDURE dbo.POS_InvoiceHeaders_select_all
AS
BEGIN
    SELECT *
    FROM POS_InvoiceHeaders;
END
GO