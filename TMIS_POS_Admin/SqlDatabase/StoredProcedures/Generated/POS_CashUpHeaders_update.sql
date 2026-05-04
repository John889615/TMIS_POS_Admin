USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_CashUpHeaders_update', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_CashUpHeaders_update;
GO

CREATE PROCEDURE dbo.POS_CashUpHeaders_update
    @CashUpHeaderID UNIQUEIDENTIFIER,
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
    UPDATE POS_CashUpHeaders
    SET     FK_CostCenterID = @FK_CostCenterID,
    FK_CurrencyID = @FK_CurrencyID,
    CashUpDate = @CashUpDate,
    CashUpBy = @CashUpBy,
    TotalSystemAmount = @TotalSystemAmount,
    TotalCountedAmount = @TotalCountedAmount,
    TotalVariance = @TotalVariance,
    Notes = @Notes,
    IsFinalised = @IsFinalised,
    DateUpdated = @DateUpdated
    WHERE CashUpHeaderID = @CashUpHeaderID;

    SELECT *
    FROM POS_CashUpHeaders
    WHERE CashUpHeaderID = @CashUpHeaderID;
END
GO