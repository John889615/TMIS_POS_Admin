USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_ProductCategories_update', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_ProductCategories_update;
GO

CREATE PROCEDURE dbo.POS_ProductCategories_update
    @ProductCategoryID INT,
    @CategoryName VARCHAR(255),
    @FK_ProductCategoryID INT = NULL,
    @BC_ID VARCHAR(255) = NULL,
    @IsMaster BIT,
    @IsActive BIT,
    @DateAdded DATETIME,
    @DateUpdated DATETIME
AS
BEGIN
    UPDATE POS_ProductCategories
    SET     CategoryName = @CategoryName,
    FK_ProductCategoryID = @FK_ProductCategoryID,
    BC_ID = @BC_ID,
    IsMaster = @IsMaster,
    IsActive = @IsActive,
    DateAdded = @DateAdded,
    DateUpdated = @DateUpdated
    WHERE ProductCategoryID = @ProductCategoryID;

    SELECT *
    FROM POS_ProductCategories
    WHERE ProductCategoryID = @ProductCategoryID;
END
GO