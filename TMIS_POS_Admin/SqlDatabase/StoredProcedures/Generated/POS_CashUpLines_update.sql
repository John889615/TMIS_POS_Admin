USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_CashUpLines_update', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_CashUpLines_update;
GO

CREATE PROCEDURE dbo.POS_CashUpLines_update
    @CashUpPaymentTypeID UNIQUEIDENTIFIER,
    @FK_CashUpID UNIQUEIDENTIFIER,
    @FK_PaymentTypeID INT,
    @SystemAmount DECIMAL (18, 4),
    @CountedAmount DECIMAL (18, 4),
    @VarianceAmount DECIMAL (18, 4) = NULL,
    @Notes VARCHAR(MAX) = NULL,
    @DateCreated DATETIME,
    @DateUpdated DATETIME
AS
BEGIN
    UPDATE POS_CashUpLines
    SET     FK_CashUpID = @FK_CashUpID,
    FK_PaymentTypeID = @FK_PaymentTypeID,
    SystemAmount = @SystemAmount,
    CountedAmount = @CountedAmount,
    VarianceAmount = @VarianceAmount,
    Notes = @Notes,
    DateUpdated = @DateUpdated
    WHERE CashUpPaymentTypeID = @CashUpPaymentTypeID;

    SELECT *
    FROM POS_CashUpLines
    WHERE CashUpPaymentTypeID = @CashUpPaymentTypeID;
END
GO