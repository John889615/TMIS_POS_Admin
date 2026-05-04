USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_ImageCategories_update', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_ImageCategories_update;
GO

CREATE PROCEDURE dbo.POS_ImageCategories_update
    @ImageCategoryID INT,
    @Category VARCHAR(50),
    @DateCreated DATETIME,
    @DateUpdated DATETIME
AS
BEGIN
    UPDATE POS_ImageCategories
    SET     Category = @Category,
    DateUpdated = @DateUpdated
    WHERE ImageCategoryID = @ImageCategoryID;

    SELECT *
    FROM POS_ImageCategories
    WHERE ImageCategoryID = @ImageCategoryID;
END
GO