USE [TMIS_Development]
GO

IF OBJECT_ID('dbo.POS_ProductCategories_insert', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_ProductCategories_insert;
GO

CREATE PROCEDURE dbo.POS_ProductCategories_insert
    @CategoryName VARCHAR(255),
    @FK_ProductCategoryID INT = NULL,
    @BC_ID VARCHAR(255) = NULL,
    @IsMaster BIT,
    @IsActive BIT,
    @DateAdded DATETIME,
    @DateUpdated DATETIME = NULL
AS
BEGIN
    DECLARE @Inserted TABLE (ProductCategoryID INT);

    INSERT INTO POS_ProductCategories (CategoryName, FK_ProductCategoryID, BC_ID, IsMaster, IsActive, DateAdded, DateUpdated)
    OUTPUT INSERTED.ProductCategoryID INTO @Inserted
    VALUES (@CategoryName, @FK_ProductCategoryID, @BC_ID, @IsMaster, @IsActive, @DateAdded, @DateUpdated);

    SELECT *
    FROM POS_ProductCategories
    WHERE ProductCategoryID = 
    (
        SELECT TOP 1 ProductCategoryID
        FROM @Inserted
    );
END
GO