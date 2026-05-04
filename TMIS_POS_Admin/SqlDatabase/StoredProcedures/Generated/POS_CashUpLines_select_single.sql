USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_CashUpLines_select_single', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_CashUpLines_select_single;
GO

CREATE PROCEDURE dbo.POS_CashUpLines_select_single
    @CashUpPaymentTypeID UNIQUEIDENTIFIER
AS
BEGIN
    SELECT *
    FROM POS_CashUpLines
    WHERE CashUpPaymentTypeID = @CashUpPaymentTypeID;
END
GO