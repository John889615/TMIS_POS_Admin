USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_Locations_select_single', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_Locations_select_single;
GO

CREATE PROCEDURE dbo.POS_Locations_select_single
    @LocationID INT
AS
BEGIN
    SELECT *
    FROM POS_Locations
    WHERE LocationID = @LocationID;
END
GO