USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.Debtors_select_single', 'P') IS NOT NULL
    DROP PROCEDURE dbo.Debtors_select_single;
GO

CREATE PROCEDURE dbo.Debtors_select_single
    @DebtorID INT
AS
BEGIN
    SELECT *
    FROM Debtors
    WHERE DebtorID = @DebtorID;
END
GO