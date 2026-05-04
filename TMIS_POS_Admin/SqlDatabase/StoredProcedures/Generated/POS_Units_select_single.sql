USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_Units_select_single', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_Units_select_single;
GO

CREATE PROCEDURE dbo.POS_Units_select_single
    @UnitID INT
AS
BEGIN
    SELECT *
    FROM POS_Units
    WHERE UnitID = @UnitID;
END
GO