USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.DebtorTypes_select_single', 'P') IS NOT NULL
    DROP PROCEDURE dbo.DebtorTypes_select_single;
GO

CREATE PROCEDURE dbo.DebtorTypes_select_single
    @DebtorTypeID INT
AS
BEGIN
    SELECT *
    FROM DebtorTypes
    WHERE DebtorTypeID = @DebtorTypeID;
END
GO