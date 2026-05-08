USE [TMIS_Development]
GO

IF OBJECT_ID('dbo.POS_InvoiceHeaders_insert', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_InvoiceHeaders_insert;
GO

CREATE PROCEDURE dbo.POS_InvoiceHeaders_insert
    @InvoiceHeaderID UNIQUEIDENTIFIER = NULL,
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
    DECLARE @Inserted TABLE (InvoiceHeaderID UNIQUEIDENTIFIER);

    INSERT INTO POS_InvoiceHeaders (InvoiceHeaderID, FK_LocationID, FK_AccountID, InvoiceNo, PartyName, BookingReference, DiscountTotal, GratuityTotal, ExclTotal, VatTotal, InclTotal, DateCreated, DatePaid, FK_CurrencyID, IsPaid, AmountPaid, AmountDue, IsVoided, VoidReason, VoidedDate, VoidedBy)
    OUTPUT INSERTED.InvoiceHeaderID INTO @Inserted
    VALUES (ISNULL(@InvoiceHeaderID, NEWID()), @FK_LocationID, @FK_AccountID, @InvoiceNo, @PartyName, @BookingReference, @DiscountTotal, @GratuityTotal, @ExclTotal, @VatTotal, @InclTotal, @DateCreated, @DatePaid, @FK_CurrencyID, @IsPaid, @AmountPaid, @AmountDue, @IsVoided, @VoidReason, @VoidedDate, @VoidedBy);

    SELECT *
    FROM POS_InvoiceHeaders
    WHERE InvoiceHeaderID = 
    (
        SELECT TOP 1 InvoiceHeaderID
        FROM @Inserted
    );
END
GO