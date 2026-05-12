USE [TMIS_Development]
GO

IF OBJECT_ID('dbo.POS_ProductExtraCategories_insert', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_ProductExtraCategories_insert;
GO

CREATE PROCEDURE dbo.POS_ProductExtraCategories_insert
    @Category VARCHAR(50),
    @DisplayOrder INT = NULL,
    @FK_CreatedUserID INT,
    @FK_UpdatedUserID INT = NULL,
    @DateCreated DATETIME,
    @DateUpdated DATETIME = NULL
AS
BEGIN
    DECLARE @Inserted TABLE (ProductExtraCategoryID INT);

    INSERT INTO POS_ProductExtraCategories (Category, DisplayOrder, FK_CreatedUserID, FK_UpdatedUserID, DateCreated, DateUpdated)
    OUTPUT INSERTED.ProductExtraCategoryID INTO @Inserted
    VALUES (@Category, @DisplayOrder, @FK_CreatedUserID, @FK_UpdatedUserID, @DateCreated, @DateUpdated);

    SELECT *
    FROM POS_ProductExtraCategories
    WHERE ProductExtraCategoryID = 
    (
        SELECT TOP 1 ProductExtraCategoryID
        FROM @Inserted
    );
END
GO