USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.StatusGroups_select_single', 'P') IS NOT NULL
    DROP PROCEDURE dbo.StatusGroups_select_single;
GO

CREATE PROCEDURE dbo.StatusGroups_select_single
    @StatusGroupID INT
AS
BEGIN
    SELECT *
    FROM StatusGroups
    WHERE StatusGroupID = @StatusGroupID;
END
GO