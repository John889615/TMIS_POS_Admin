USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_Menus_update', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_Menus_update;
GO

CREATE PROCEDURE dbo.POS_Menus_update
    @MenuID INT,
    @MenuName VARCHAR(50),
    @IsActive BIT,
    @DateCreated DATETIME,
    @DateUpdated DATETIME = NULL
AS
BEGIN
    UPDATE POS_Menus
    SET     MenuName = @MenuName,
    IsActive = @IsActive,
    DateUpdated = @DateUpdated
    WHERE MenuID = @MenuID;

    SELECT *
    FROM POS_Menus
    WHERE MenuID = @MenuID;
END
GO