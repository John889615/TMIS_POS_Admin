USE [TMIS_Development]
GO

IF OBJECT_ID('dbo.POS_InvoiceLines_insert', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_InvoiceLines_insert;
GO

CREATE PROCEDURE dbo.POS_InvoiceLines_insert
    @FK_InvoiceTabID UNIQUEIDENTIFIER,
    @FK_ProductID INT,
    @Product VARCHAR(100),
    @Quantity DECIMAL (18, 4),
    @LineDiscount DECIMAL (18, 4),
    @LineTotalExcl DECIMAL (18, 4),
    @LineTotalVat DECIMAL (18, 4),
    @LineTotalIncl DECIMAL (18, 4),
    @Guests VARCHAR(100) = NULL
AS
BEGIN
    DECLARE @Inserted TABLE (InvoiceLineID UNIQUEIDENTIFIER);

    INSERT INTO POS_InvoiceLines (FK_InvoiceTabID, FK_ProductID, Product, Quantity, LineDiscount, LineTotalExcl, LineTotalVat, LineTotalIncl, Guests)
    OUTPUT INSERTED.InvoiceLineID INTO @Inserted
    VALUES (@FK_InvoiceTabID, @FK_ProductID, @Product, @Quantity, @LineDiscount, @LineTotalExcl, @LineTotalVat, @LineTotalIncl, @Guests);

    SELECT *
    FROM POS_InvoiceLines
    WHERE InvoiceLineID = 
    (
        SELECT TOP 1 InvoiceLineID
        FROM @Inserted
    );
END
GO