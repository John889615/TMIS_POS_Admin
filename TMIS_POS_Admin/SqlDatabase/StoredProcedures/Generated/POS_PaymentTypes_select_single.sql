USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_PaymentTypes_select_single', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_PaymentTypes_select_single;
GO

CREATE PROCEDURE dbo.POS_PaymentTypes_select_single
    @PaymentTypeID INT
AS
BEGIN
    SELECT *
    FROM POS_PaymentTypes
    WHERE PaymentTypeID = @PaymentTypeID;
END
GO