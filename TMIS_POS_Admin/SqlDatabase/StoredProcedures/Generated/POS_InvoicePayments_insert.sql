USE [TMIS_Development]
GO

IF OBJECT_ID('dbo.POS_InvoicePayments_insert', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_InvoicePayments_insert;
GO

CREATE PROCEDURE dbo.POS_InvoicePayments_insert
    @InvoicePaymentID UNIQUEIDENTIFIER = NULL,
    @FK_InvoiceID UNIQUEIDENTIFIER,
    @FK_PaymentTypeID INT,
    @FK_BaseCurrencyID INT,
    @FK_PaymentCurrencyID INT,
    @BaseCurrencyCode VARCHAR(10),
    @PaymentCurrencyCode VARCHAR(10),
    @BaseAmountPaid DECIMAL (18, 4),
    @PaymentAmountPaid DECIMAL (18, 4),
    @ExchangeRate DECIMAL (18, 4),
    @ExchangeDate DATETIME,
    @DatePaid DATETIME,
    @StaffName VARCHAR(255),
    @IdempotencyKey UNIQUEIDENTIFIER,
    @Reference VARCHAR(100) = NULL,
    @Notes VARCHAR(MAX) = NULL,
    @IsVoided BIT,
    @VoidReason VARCHAR(255) = NULL,
    @VoidedDate DATETIME = NULL,
    @VoidedBy VARCHAR(255) = NULL,
    @SignatureBase64 VARCHAR(MAX) = NULL
AS
BEGIN
    DECLARE @Inserted TABLE (InvoicePaymentID UNIQUEIDENTIFIER);

    INSERT INTO POS_InvoicePayments (InvoicePaymentID, FK_InvoiceID, FK_PaymentTypeID, FK_BaseCurrencyID, FK_PaymentCurrencyID, BaseCurrencyCode, PaymentCurrencyCode, BaseAmountPaid, PaymentAmountPaid, ExchangeRate, ExchangeDate, DatePaid, StaffName, IdempotencyKey, Reference, Notes, IsVoided, VoidReason, VoidedDate, VoidedBy, SignatureBase64)
    OUTPUT INSERTED.InvoicePaymentID INTO @Inserted
    VALUES (ISNULL(@InvoicePaymentID, NEWID()), @FK_InvoiceID, @FK_PaymentTypeID, @FK_BaseCurrencyID, @FK_PaymentCurrencyID, @BaseCurrencyCode, @PaymentCurrencyCode, @BaseAmountPaid, @PaymentAmountPaid, @ExchangeRate, @ExchangeDate, @DatePaid, @StaffName, @IdempotencyKey, @Reference, @Notes, @IsVoided, @VoidReason, @VoidedDate, @VoidedBy, @SignatureBase64);

    SELECT *
    FROM POS_InvoicePayments
    WHERE InvoicePaymentID = 
    (
        SELECT TOP 1 InvoicePaymentID
        FROM @Inserted
    );
END
GO