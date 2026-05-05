USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_ProductExtraCategories_update', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_ProductExtraCategories_update;
GO

CREATE PROCEDURE dbo.POS_ProductExtraCategories_update
    @ProductExtraCategoryID INT,
    @Category VARCHAR(50),
    @DisplayOrder INT = NULL,
    @FK_CreatedUserID INT,
    @FK_UpdatedUserID INT = NULL,
    @DateCreated DATETIME,
    @DateUpdated DATETIME
AS
BEGIN
    UPDATE POS_ProductExtraCategories
    SET     Category = @Category,
    DisplayOrder = @DisplayOrder,
    FK_CreatedUserID = @FK_CreatedUserID,
    FK_UpdatedUserID = @FK_UpdatedUserID,
    DateUpdated = @DateUpdated
    WHERE ProductExtraCategoryID = @ProductExtraCategoryID;

    SELECT *
    FROM POS_ProductExtraCategories
    WHERE ProductExtraCategoryID = @ProductExtraCategoryID;
END
GO