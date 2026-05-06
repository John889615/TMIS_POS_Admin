USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_InvoiceLines_update', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_InvoiceLines_update;
GO

CREATE PROCEDURE dbo.POS_InvoiceLines_update
    @InvoiceLineID UNIQUEIDENTIFIER,
    @FK_InvoiceTabID UNIQUEIDENTIFIER,
    @FK_ProductID INT = NULL,
    @Product VARCHAR(100),
    @Quantity DECIMAL (18, 4),
    @LineDiscount DECIMAL (18, 4),
    @LineTotalExcl DECIMAL (18, 4),
    @LineTotalVat DECIMAL (18, 4),
    @LineTotalIncl DECIMAL (18, 4),
    @Guests VARCHAR(100) = NULL
AS
BEGIN
    UPDATE POS_InvoiceLines
    SET     FK_InvoiceTabID = @FK_InvoiceTabID,
    FK_ProductID = @FK_ProductID,
    Product = @Product,
    Quantity = @Quantity,
    LineDiscount = @LineDiscount,
    LineTotalExcl = @LineTotalExcl,
    LineTotalVat = @LineTotalVat,
    LineTotalIncl = @LineTotalIncl,
    Guests = @Guests
    WHERE InvoiceLineID = @InvoiceLineID;

    SELECT *
    FROM POS_InvoiceLines
    WHERE InvoiceLineID = @InvoiceLineID;
END
GO