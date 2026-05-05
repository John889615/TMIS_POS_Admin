USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.Entities_select_single', 'P') IS NOT NULL
    DROP PROCEDURE dbo.Entities_select_single;
GO

CREATE PROCEDURE dbo.Entities_select_single
    @EntityID INT
AS
BEGIN
    SELECT *
    FROM Entities
    WHERE EntityID = @EntityID;
END
GO