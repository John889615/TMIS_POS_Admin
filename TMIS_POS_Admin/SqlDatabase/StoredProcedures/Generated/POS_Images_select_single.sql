USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_Images_select_single', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_Images_select_single;
GO

CREATE PROCEDURE dbo.POS_Images_select_single
    @ImageID INT
AS
BEGIN
    SELECT *
    FROM POS_Images
    WHERE ImageID = @ImageID;
END
GO