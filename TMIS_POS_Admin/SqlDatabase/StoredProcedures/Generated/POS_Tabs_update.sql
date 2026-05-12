USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_Tabs_update', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_Tabs_update;
GO

CREATE PROCEDURE dbo.POS_Tabs_update
    @TabID UNIQUEIDENTIFIER,
    @FK_LocationID INT,
    @FK_AccountID UNIQUEIDENTIFIER = NULL,
    @FK_CostCenterID INT = NULL,
    @FK_PaymentTypeID INT = NULL,
    @FK_CurrencyID INT = NULL,
    @TabName VARCHAR(50) = NULL,
    @TableName INT = NULL,
    @NoOfGuests INT = NULL,
    @Gratuity DECIMAL (18, 4) = NULL,
    @GratuityPerc DECIMAL (18, 4) = NULL,
    @Discount DECIMAL (18, 4) = NULL,
    @DiscountPerc DECIMAL (18, 4) = NULL,
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
    @DateUpdated DATETIME = NULL,
    @TableNumber INT = NULL
AS
BEGIN
    UPDATE POS_Tabs
    SET     FK_LocationID = @FK_LocationID,
    FK_AccountID = @FK_AccountID,
    FK_CostCenterID = @FK_CostCenterID,
    FK_PaymentTypeID = @FK_PaymentTypeID,
    FK_CurrencyID = @FK_CurrencyID,
    TabName = @TabName,
    TableName = @TableName,
    NoOfGuests = @NoOfGuests,
    Gratuity = @Gratuity,
    GratuityPerc = @GratuityPerc,
    Discount = @Discount,
    DiscountPerc = @DiscountPerc,
    IsVoided = @IsVoided,
    VoidNote = @VoidNote,
    IsPaid = @IsPaid,
    AmountPaid = @AmountPaid,
    AmountDue = @AmountDue,
    VatTotal = @VatTotal,
    CurrentExchangeRate = @CurrentExchangeRate,
    PaymentDate = @PaymentDate,
    ClosedDate = @ClosedDate,
    AdditionalInfo = @AdditionalInfo,
    CreatedBy = @CreatedBy,
    DateUpdated = @DateUpdated,
    TableNumber = @TableNumber
    WHERE TabID = @TabID;

    SELECT *
    FROM POS_Tabs
    WHERE TabID = @TabID;
END
GO