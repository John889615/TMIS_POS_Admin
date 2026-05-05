USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_CashUpHeaders_select_single', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_CashUpHeaders_select_single;
GO

CREATE PROCEDURE dbo.POS_CashUpHeaders_select_single
    @CashUpHeaderID UNIQUEIDENTIFIER
AS
BEGIN
    SELECT *
    FROM POS_CashUpHeaders
    WHERE CashUpHeaderID = @CashUpHeaderID;
END
GO