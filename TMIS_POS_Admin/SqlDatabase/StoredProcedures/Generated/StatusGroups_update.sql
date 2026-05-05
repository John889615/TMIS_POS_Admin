USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.StatusGroups_update', 'P') IS NOT NULL
    DROP PROCEDURE dbo.StatusGroups_update;
GO

CREATE PROCEDURE dbo.StatusGroups_update
    @StatusGroupID INT,
    @GroupName VARCHAR(100),
    @Description NVARCHAR(500) = NULL
AS
BEGIN
    UPDATE StatusGroups
    SET     GroupName = @GroupName,
    Description = @Description
    WHERE StatusGroupID = @StatusGroupID;

    SELECT *
    FROM StatusGroups
    WHERE StatusGroupID = @StatusGroupID;
END
GO