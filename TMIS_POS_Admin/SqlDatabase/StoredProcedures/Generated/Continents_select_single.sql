USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.Continents_select_single', 'P') IS NOT NULL
    DROP PROCEDURE dbo.Continents_select_single;
GO

CREATE PROCEDURE dbo.Continents_select_single
    @ContinentID INT
AS
BEGIN
    SELECT *
    FROM Continents
    WHERE ContinentID = @ContinentID;
END
GO