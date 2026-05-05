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
    @PartyName VARCHAR(50) = NULL,
    @BookingReference VARCHAR(50) = NULL,
    @DiscountTotal DECIMAL (18, 4),
    @GratuityTotal DECIMAL (18, 4),
    @ExclTotal DECIMAL (18, 4),
    @VatTotal DECIMAL (18, 4),
    @InclTotal DECIMAL (18, 4),
    @IsDiscarded BIT,
    @BC_InvoiceID VARCHAR(255) = NULL,
    @DateCreated DATETIME,
    @DatePaid DATETIME = NULL,
    @SyncedToServer BIT
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
    IsDiscarded = @IsDiscarded,
    BC_InvoiceID = @BC_InvoiceID,
    DatePaid = @DatePaid,
    SyncedToServer = @SyncedToServer
    WHERE InvoiceHeaderID = @InvoiceHeaderID;

    SELECT *
    FROM POS_InvoiceHeaders
    WHERE InvoiceHeaderID = @InvoiceHeaderID;
END
GO