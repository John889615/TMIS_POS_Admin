USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_InvoiceHeaders_update', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_InvoiceHeaders_update;
GO

CREATE PROCEDURE dbo.POS_InvoiceHeaders_update
    @InvoiceHeaderID UNIQUEIDENTIFIER,
    @FK_LocationID INT,
    @FK_AccountID UNIQUEIDENTIFIER = NULL,
    @InvoiceNo VARCHAR(50),
    @PartyName VARCHAR(100) = NULL,
    @BookingReference VARCHAR(100) = NULL,
    @DiscountTotal DECIMAL (18, 4),
    @GratuityTotal DECIMAL (18, 4),
    @ExclTotal DECIMAL (18, 4),
    @VatTotal DECIMAL (18, 4),
    @InclTotal DECIMAL (18, 4),
    @DateCreated DATETIME,
    @DatePaid DATETIME = NULL,
    @FK_CurrencyID INT,
    @IsPaid BIT,
    @AmountPaid DECIMAL (18, 4),
    @AmountDue DECIMAL (18, 4),
    @IsVoided BIT,
    @VoidReason VARCHAR(255) = NULL,
    @VoidedDate DATETIME = NULL,
    @VoidedBy VARCHAR(255) = NULL
AS
BEGIN
    UPDATE POS_InvoiceHeaders
    SET     FK_LocationID = @FK_LocationID,
    FK_AccountID = @FK_AccountID,
    InvoiceNo = @InvoiceNo,
    PartyName = @PartyName,
    BookingReference = @BookingReference,
    DiscountTotal = @DiscountTotal,
    GratuityTotal = @GratuityTotal,
    ExclTotal = @ExclTotal,
    VatTotal = @VatTotal,
    InclTotal = @InclTotal,
    DatePaid = @DatePaid,
    FK_CurrencyID = @FK_CurrencyID,
    IsPaid = @IsPaid,
    AmountPaid = @AmountPaid,
    AmountDue = @AmountDue,
    IsVoided = @IsVoided,
    VoidReason = @VoidReason,
    VoidedDate = @VoidedDate,
    VoidedBy = @VoidedBy
    WHERE InvoiceHeaderID = @InvoiceHeaderID;

    SELECT *
    FROM POS_InvoiceHeaders
    WHERE InvoiceHeaderID = @InvoiceHeaderID;
END
GO