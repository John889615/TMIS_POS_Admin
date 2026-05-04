USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_ServedAs_select_single', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_ServedAs_select_single;
GO

CREATE PROCEDURE dbo.POS_ServedAs_select_single
    @ServedAsID INT
AS
BEGIN
    SELECT *
    FROM POS_ServedAs
    WHERE ServedAsID = @ServedAsID;
END
GO