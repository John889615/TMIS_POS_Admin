USE [TMIS_Development]
GO

IF OBJECT_ID('dbo.POS_CashUpLines_insert', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_CashUpLines_insert;
GO

CREATE PROCEDURE dbo.POS_CashUpLines_insert
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
    DECLARE @Inserted TABLE (CashUpPaymentTypeID UNIQUEIDENTIFIER);

    INSERT INTO POS_CashUpLines (FK_CashUpID, FK_PaymentTypeID, SystemAmount, CountedAmount, VarianceAmount, Notes, DateCreated, DateUpdated)
    OUTPUT INSERTED.CashUpPaymentTypeID INTO @Inserted
    VALUES (@FK_CashUpID, @FK_PaymentTypeID, @SystemAmount, @CountedAmount, @VarianceAmount, @Notes, @DateCreated, @DateUpdated);

    SELECT *
    FROM POS_CashUpLines
    WHERE CashUpPaymentTypeID = 
    (
        SELECT TOP 1 CashUpPaymentTypeID
        FROM @Inserted
    );
END
GO