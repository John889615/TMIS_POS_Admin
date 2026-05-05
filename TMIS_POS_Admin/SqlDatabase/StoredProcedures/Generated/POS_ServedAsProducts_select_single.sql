USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_ServedAsProducts_select_single', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_ServedAsProducts_select_single;
GO

CREATE PROCEDURE dbo.POS_ServedAsProducts_select_single
    @ServedAsProductID INT
AS
BEGIN
    SELECT *
    FROM POS_ServedAsProducts
    WHERE ServedAsProductID = @ServedAsProductID;
END
GO