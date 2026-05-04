USE [TMIS_Development]
GO

IF OBJECT_ID('dbo.POS_Tabs_insert', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_Tabs_insert;
GO

CREATE PROCEDURE dbo.POS_Tabs_insert
    @FK_LocationID INT,
    @FK_AccountID UNIQUEIDENTIFIER = NULL,
    @FK_CostCenterID INT = NULL,
    @FK_PaymentTypeID INT = NULL,
    @FK_CurrencyID INT = NULL,
    @TabName VARCHAR(50) = NULL,
    @TableName INT = NULL,
    @NoOfGuests INT = NULL,
    @Gratuity DECIMAL (18, 4) = NULL,
    @GratuityPerc INT = NULL,
    @Discount DECIMAL (18, 4) = NULL,
    @DiscountPerc INT = NULL,
    @IsVoided BIT,
    @VoidNote VARCHAR(MAX) = NULL,
    @IsPaid BIT,
    @AmountPaid DECIMAL (18, 4),
    @AmountDue DECIMAL (18, 4) = NULL,
    @VatTotal DECIMAL (18, 4) = NULL,
    @CurrentExchangeRate DECIMAL (18, 4) = NULL,
    @PaymentDate DATETIME = NULL,
    @ClosedDate DATETIME = NULL,
    @AdditionalInfo VARCHAR(255) = NULL,
    @CreatedBy VARCHAR(255),
    @DateCreated DATETIME,
    @DateUpdated DATETIME
AS
BEGIN
    DECLARE @Inserted TABLE (TabID UNIQUEIDENTIFIER);

    INSERT INTO POS_Tabs (FK_LocationID, FK_AccountID, FK_CostCenterID, FK_PaymentTypeID, FK_CurrencyID, TabName, TableName, NoOfGuests, Gratuity, GratuityPerc, Discount, DiscountPerc, IsVoided, VoidNote, IsPaid, AmountPaid, AmountDue, VatTotal, CurrentExchangeRate, PaymentDate, ClosedDate, AdditionalInfo, CreatedBy, DateCreated, DateUpdated)
    OUTPUT INSERTED.TabID INTO @Inserted
    VALUES (@FK_LocationID, @FK_AccountID, @FK_CostCenterID, @FK_PaymentTypeID, @FK_CurrencyID, @TabName, @TableName, @NoOfGuests, @Gratuity, @GratuityPerc, @Discount, @DiscountPerc, @IsVoided, @VoidNote, @IsPaid, @AmountPaid, @AmountDue, @VatTotal, @CurrentExchangeRate, @PaymentDate, @ClosedDate, @AdditionalInfo, @CreatedBy, @DateCreated, @DateUpdated);

    SELECT *
    FROM POS_Tabs
    WHERE TabID = 
    (
        SELECT TOP 1 TabID
        FROM @Inserted
    );
END
GO