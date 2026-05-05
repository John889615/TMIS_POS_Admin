USE [TMIS_Development]
GO

IF OBJECT_ID('dbo.POS_ImageCategories_insert', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_ImageCategories_insert;
GO

CREATE PROCEDURE dbo.POS_ImageCategories_insert
    @Category VARCHAR(50),
    @DateCreated DATETIME,
    @DateUpdated DATETIME
AS
BEGIN
    DECLARE @Inserted TABLE (ImageCategoryID INT);

    INSERT INTO POS_ImageCategories (Category, DateCreated, DateUpdated)
    OUTPUT INSERTED.ImageCategoryID INTO @Inserted
    VALUES (@Category, @DateCreated, @DateUpdated);

    SELECT *
    FROM POS_ImageCategories
    WHERE ImageCategoryID = 
    (
        SELECT TOP 1 ImageCategoryID
        FROM @Inserted
    );
END
GO