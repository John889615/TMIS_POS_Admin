USE [TMIS_Development]
GO

IF OBJECT_ID('dbo.POS_Menus_insert', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_Menus_insert;
GO

CREATE PROCEDURE dbo.POS_Menus_insert
    @MenuName VARCHAR(50),
    @IsActive BIT,
    @DateCreated DATETIME,
    @DateUpdated DATETIME = NULL
AS
BEGIN
    DECLARE @Inserted TABLE (MenuID INT);

    INSERT INTO POS_Menus (MenuName, IsActive, DateCreated, DateUpdated)
    OUTPUT INSERTED.MenuID INTO @Inserted
    VALUES (@MenuName, @IsActive, @DateCreated, @DateUpdated);

    SELECT *
    FROM POS_Menus
    WHERE MenuID = 
    (
        SELECT TOP 1 MenuID
        FROM @Inserted
    );
END
GO