USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_TablineSubstitutes_select_single', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_TablineSubstitutes_select_single;
GO

CREATE PROCEDURE dbo.POS_TablineSubstitutes_select_single
    @POS_TablineSubstituteID UNIQUEIDENTIFIER
AS
BEGIN
    SELECT *
    FROM POS_TablineSubstitutes
    WHERE POS_TablineSubstituteID = @POS_TablineSubstituteID;
END
GO