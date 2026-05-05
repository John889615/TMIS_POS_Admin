USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_PaymentTypes_update', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_PaymentTypes_update;
GO

CREATE PROCEDURE dbo.POS_PaymentTypes_update
    @PaymentTypeID INT,
    @FK_PaymentTypeIcon INT,
    @Name VARCHAR(255),
    @IsActive BIT,
    @IsPrimary BIT,
    @IsSecondary BIT,
    @SettlePayment BIT,
    @RequireAdditionalInfo BIT,
    @RequireElevation BIT,
    @DateCreated DATETIME,
    @DateUpdated DATETIME
AS
BEGIN
    UPDATE POS_PaymentTypes
    SET     FK_PaymentTypeIcon = @FK_PaymentTypeIcon,
    [Name] = @Name,
    IsActive = @IsActive,
    IsPrimary = @IsPrimary,
    IsSecondary = @IsSecondary,
    SettlePayment = @SettlePayment,
    RequireAdditionalInfo = @RequireAdditionalInfo,
    RequireElevation = @RequireElevation,
    DateUpdated = @DateUpdated
    WHERE PaymentTypeID = @PaymentTypeID;

    SELECT *
    FROM POS_PaymentTypes
    WHERE PaymentTypeID = @PaymentTypeID;
END
GO