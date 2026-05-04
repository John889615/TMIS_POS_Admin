USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_Accounts_select_single', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_Accounts_select_single;
GO

CREATE PROCEDURE dbo.POS_Accounts_select_single
    @AccountID UNIQUEIDENTIFIER
AS
BEGIN
    SELECT *
    FROM POS_Accounts
    WHERE AccountID = @AccountID;
END
GO