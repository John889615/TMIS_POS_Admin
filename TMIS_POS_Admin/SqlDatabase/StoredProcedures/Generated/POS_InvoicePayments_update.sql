USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_InvoicePayments_update', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_InvoicePayments_update;
GO

CREATE PROCEDURE dbo.POS_InvoicePayments_update
    @InvoicePaymentID UNIQUEIDENTIFIER,
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
    UPDATE POS_InvoicePayments
    SET     FK_InvoiceID = @FK_InvoiceID,
    FK_PaymentTypeID = @FK_PaymentTypeID,
    FK_BaseCurrencyID = @FK_BaseCurrencyID,
    FK_PaymentCurrencyID = @FK_PaymentCurrencyID,
    BaseCurrencyCode = @BaseCurrencyCode,
    PaymentCurrencyCode = @PaymentCurrencyCode,
    BaseAmountPaid = @BaseAmountPaid,
    PaymentAmountPaid = @PaymentAmountPaid,
    ExchangeRate = @ExchangeRate,
    ExchangeDate = @ExchangeDate,
    DatePaid = @DatePaid,
    StaffName = @StaffName,
    IdempotencyKey = @IdempotencyKey,
    Reference = @Reference,
    Notes = @Notes,
    IsVoided = @IsVoided,
    VoidReason = @VoidReason,
    VoidedDate = @VoidedDate,
    VoidedBy = @VoidedBy,
    SignatureBase64 = @SignatureBase64
    WHERE InvoicePaymentID = @InvoicePaymentID;

    SELECT *
    FROM POS_InvoicePayments
    WHERE InvoicePaymentID = @InvoicePaymentID;
END
GO