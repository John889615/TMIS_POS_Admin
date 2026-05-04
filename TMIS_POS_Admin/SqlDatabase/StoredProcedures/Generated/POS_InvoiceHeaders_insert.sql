USE [TMIS_Development]
GO

IF OBJECT_ID('dbo.POS_InvoiceHeaders_insert', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_InvoiceHeaders_insert;
GO

CREATE PROCEDURE dbo.POS_InvoiceHeaders_insert
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
    DECLARE @Inserted TABLE (InvoiceHeaderID UNIQUEIDENTIFIER);

    INSERT INTO POS_InvoiceHeaders (FK_LocationID, FK_AccountID, InvoiceNo, PartyName, BookingReference, DiscountTotal, GratuityTotal, ExclTotal, VatTotal, InclTotal, IsDiscarded, BC_InvoiceID, DateCreated, DatePaid, SyncedToServer)
    OUTPUT INSERTED.InvoiceHeaderID INTO @Inserted
    VALUES (@FK_LocationID, @FK_AccountID, @InvoiceNo, @PartyName, @BookingReference, @DiscountTotal, @GratuityTotal, @ExclTotal, @VatTotal, @InclTotal, @IsDiscarded, @BC_InvoiceID, @DateCreated, @DatePaid, @SyncedToServer);

    SELECT *
    FROM POS_InvoiceHeaders
    WHERE InvoiceHeaderID = 
    (
        SELECT TOP 1 InvoiceHeaderID
        FROM @Inserted
    );
END
GO