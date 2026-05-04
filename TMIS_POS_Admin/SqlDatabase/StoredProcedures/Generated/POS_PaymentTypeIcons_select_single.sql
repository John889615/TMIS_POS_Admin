USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_PaymentTypeIcons_select_single', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_PaymentTypeIcons_select_single;
GO

CREATE PROCEDURE dbo.POS_PaymentTypeIcons_select_single
    @PaymentTypeIconID INT
AS
BEGIN
    SELECT *
    FROM POS_PaymentTypeIcons
    WHERE PaymentTypeIconID = @PaymentTypeIconID;
END
GO