USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_SlipTypes_select_single', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_SlipTypes_select_single;
GO

CREATE PROCEDURE dbo.POS_SlipTypes_select_single
    @SlipTypeID INT
AS
BEGIN
    SELECT *
    FROM POS_SlipTypes
    WHERE SlipTypeID = @SlipTypeID;
END
GO