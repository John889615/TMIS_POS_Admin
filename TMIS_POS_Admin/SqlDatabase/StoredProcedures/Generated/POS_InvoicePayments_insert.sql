USE [TMIS_Development]
GO

IF OBJECT_ID('dbo.POS_InvoicePayments_insert', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_InvoicePayments_insert;
GO

CREATE PROCEDURE dbo.POS_InvoicePayments_insert
    @FK_InvoiceID UNIQUEIDENTIFIER = NULL,
    @FK_PaymentTypeID INT,
    @FK_FromCurrencyID INT = NULL,
    @FK_ToCurrencyID INT = NULL,
    @FromCurrency VARCHAR(10),
    @ToCurrency VARCHAR(10),
    @FromTotal DECIMAL (18, 4) = NULL,
    @ToTotal DECIMAL (18, 4) = NULL,
    @FromAmountPaid DECIMAL (18, 4),
    @ToAmountPaid DECIMAL (18, 4),
    @ExchangeRate DECIMAL (18, 4) = NULL,
    @ExchangeDate DATETIME = NULL,
    @DatePaid DATETIME = NULL
AS
BEGIN
    DECLARE @Inserted TABLE (InvoicePaymentID UNIQUEIDENTIFIER);

    INSERT INTO POS_InvoicePayments (FK_InvoiceID, FK_PaymentTypeID, FK_FromCurrencyID, FK_ToCurrencyID, FromCurrency, ToCurrency, FromTotal, ToTotal, FromAmountPaid, ToAmountPaid, ExchangeRate, ExchangeDate, DatePaid)
    OUTPUT INSERTED.InvoicePaymentID INTO @Inserted
    VALUES (@FK_InvoiceID, @FK_PaymentTypeID, @FK_FromCurrencyID, @FK_ToCurrencyID, @FromCurrency, @ToCurrency, @FromTotal, @ToTotal, @FromAmountPaid, @ToAmountPaid, @ExchangeRate, @ExchangeDate, @DatePaid);

    SELECT *
    FROM POS_InvoicePayments
    WHERE InvoicePaymentID = 
    (
        SELECT TOP 1 InvoicePaymentID
        FROM @Inserted
    );
END
GO