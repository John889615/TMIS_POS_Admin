USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_TablineSubstitutes_select_single', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_TablineSubstitutes_select_single;
GO

CREATE PROCEDURE dbo.POS_TablineSubstitutes_select_single
    @TablineSubstituteID UNIQUEIDENTIFIER
AS
BEGIN
    SELECT *
    FROM POS_TablineSubstitutes
    WHERE TablineSubstituteID = @TablineSubstituteID;
END
GO