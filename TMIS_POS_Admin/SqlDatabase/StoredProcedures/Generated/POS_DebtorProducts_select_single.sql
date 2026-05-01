USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_DebtorProducts_select_single', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_DebtorProducts_select_single;
GO

CREATE PROCEDURE dbo.POS_DebtorProducts_select_single
    @DebtorProductID INT
AS
BEGIN
    SELECT *
    FROM POS_DebtorProducts
    WHERE DebtorProductID = @DebtorProductID;
END
GO