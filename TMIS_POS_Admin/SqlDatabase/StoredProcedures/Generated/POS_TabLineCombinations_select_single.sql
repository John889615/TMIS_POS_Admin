USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_TabLineCombinations_select_single', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_TabLineCombinations_select_single;
GO

CREATE PROCEDURE dbo.POS_TabLineCombinations_select_single
    @TabLineCombinationID UNIQUEIDENTIFIER
AS
BEGIN
    SELECT *
    FROM POS_TabLineCombinations
    WHERE TabLineCombinationID = @TabLineCombinationID;
END
GO