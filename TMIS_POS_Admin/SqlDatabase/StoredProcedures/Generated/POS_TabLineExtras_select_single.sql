USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_TabLineExtras_select_single', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_TabLineExtras_select_single;
GO

CREATE PROCEDURE dbo.POS_TabLineExtras_select_single
    @TabLineExtraID UNIQUEIDENTIFIER
AS
BEGIN
    SELECT *
    FROM POS_TabLineExtras
    WHERE TabLineExtraID = @TabLineExtraID;
END
GO