USE [TMIS_Development]
GO

IF OBJECT_ID('dbo.POS_CashUpHeaders_insert', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_CashUpHeaders_insert;
GO

CREATE PROCEDURE dbo.POS_CashUpHeaders_insert
    @FK_CostCenterID INT,
    @FK_CurrencyID INT,
    @CashUpDate DATE,
    @CashUpBy VARCHAR(255) = NULL,
    @TotalSystemAmount DECIMAL (18, 4) = NULL,
    @TotalCountedAmount DECIMAL (18, 4) = NULL,
    @TotalVariance DECIMAL (18, 4) = NULL,
    @Notes VARCHAR(MAX) = NULL,
    @IsFinalised BIT,
    @DateCreated DATETIME,
    @DateUpdated DATETIME
AS
BEGIN
    DECLARE @Inserted TABLE (CashUpHeaderID UNIQUEIDENTIFIER);

    INSERT INTO POS_CashUpHeaders (FK_CostCenterID, FK_CurrencyID, CashUpDate, CashUpBy, TotalSystemAmount, TotalCountedAmount, TotalVariance, Notes, IsFinalised, DateCreated, DateUpdated)
    OUTPUT INSERTED.CashUpHeaderID INTO @Inserted
    VALUES (@FK_CostCenterID, @FK_CurrencyID, @CashUpDate, @CashUpBy, @TotalSystemAmount, @TotalCountedAmount, @TotalVariance, @Notes, @IsFinalised, @DateCreated, @DateUpdated);

    SELECT *
    FROM POS_CashUpHeaders
    WHERE CashUpHeaderID = 
    (
        SELECT TOP 1 CashUpHeaderID
        FROM @Inserted
    );
END
GO