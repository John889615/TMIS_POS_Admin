USE [TMIS_Development]
GO

IF OBJECT_ID('dbo.POS_PaymentTypes_insert', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_PaymentTypes_insert;
GO

CREATE PROCEDURE dbo.POS_PaymentTypes_insert
    @FK_PaymentTypeIcon INT,
    @Name VARCHAR(255),
    @IsActive BIT,
    @IsPrimary BIT,
    @IsSecondary BIT,
    @SettlePayment BIT,
    @RequireAdditionalInfo BIT,
    @RequireElevation BIT,
    @DateCreated DATETIME,
    @DateUpdated DATETIME = NULL
AS
BEGIN
    DECLARE @Inserted TABLE (PaymentTypeID INT);

    INSERT INTO POS_PaymentTypes (FK_PaymentTypeIcon, [Name], IsActive, IsPrimary, IsSecondary, SettlePayment, RequireAdditionalInfo, RequireElevation, DateCreated, DateUpdated)
    OUTPUT INSERTED.PaymentTypeID INTO @Inserted
    VALUES (@FK_PaymentTypeIcon, @Name, @IsActive, @IsPrimary, @IsSecondary, @SettlePayment, @RequireAdditionalInfo, @RequireElevation, @DateCreated, @DateUpdated);

    SELECT *
    FROM POS_PaymentTypes
    WHERE PaymentTypeID = 
    (
        SELECT TOP 1 PaymentTypeID
        FROM @Inserted
    );
END
GO