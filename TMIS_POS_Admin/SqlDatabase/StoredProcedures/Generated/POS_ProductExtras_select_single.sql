USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_ProductExtras_select_single', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_ProductExtras_select_single;
GO

CREATE PROCEDURE dbo.POS_ProductExtras_select_single
    @ProductExtraID INT
AS
BEGIN
    SELECT *
    FROM POS_ProductExtras
    WHERE ProductExtraID = @ProductExtraID;
END
GO