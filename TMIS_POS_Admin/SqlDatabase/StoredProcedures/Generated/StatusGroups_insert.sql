USE [TMIS_Development]
GO

IF OBJECT_ID('dbo.StatusGroups_insert', 'P') IS NOT NULL
    DROP PROCEDURE dbo.StatusGroups_insert;
GO

CREATE PROCEDURE dbo.StatusGroups_insert
    @GroupName VARCHAR(100),
    @Description NVARCHAR(500) = NULL
AS
BEGIN
    DECLARE @Inserted TABLE (StatusGroupID INT);

    INSERT INTO StatusGroups (GroupName, Description)
    OUTPUT INSERTED.StatusGroupID INTO @Inserted
    VALUES (@GroupName, @Description);

    SELECT *
    FROM StatusGroups
    WHERE StatusGroupID = 
    (
        SELECT TOP 1 StatusGroupID
        FROM @Inserted
    );
END
GO