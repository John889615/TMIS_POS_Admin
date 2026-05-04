USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_Arrivals_select_single', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_Arrivals_select_single;
GO

CREATE PROCEDURE dbo.POS_Arrivals_select_single
    @ArrivalID UNIQUEIDENTIFIER
AS
BEGIN
    SELECT *
    FROM POS_Arrivals
    WHERE ArrivalID = @ArrivalID;
END
GO