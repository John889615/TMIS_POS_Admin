USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_InvoicePayments_update', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_InvoicePayments_update;
GO

CREATE PROCEDURE dbo.POS_InvoicePayments_update
    @InvoicePaymentID UNIQUEIDENTIFIER,
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
    UPDATE POS_InvoicePayments
    SET     FK_InvoiceID = @FK_InvoiceID,
    FK_PaymentTypeID = @FK_PaymentTypeID,
    FK_FromCurrencyID = @FK_FromCurrencyID,
    FK_ToCurrencyID = @FK_ToCurrencyID,
    FromCurrency = @FromCurrency,
    ToCurrency = @ToCurrency,
    FromTotal = @FromTotal,
    ToTotal = @ToTotal,
    FromAmountPaid = @FromAmountPaid,
    ToAmountPaid = @ToAmountPaid,
    ExchangeRate = @ExchangeRate,
    ExchangeDate = @ExchangeDate,
    DatePaid = @DatePaid
    WHERE InvoicePaymentID = @InvoicePaymentID;

    SELECT *
    FROM POS_InvoicePayments
    WHERE InvoicePaymentID = @InvoicePaymentID;
END
GO