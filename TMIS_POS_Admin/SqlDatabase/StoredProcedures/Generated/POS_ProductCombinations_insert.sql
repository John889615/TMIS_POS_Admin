USE [TMIS_Development]
GO

IF OBJECT_ID('dbo.POS_ProductCombinations_insert', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_ProductCombinations_insert;
GO

CREATE PROCEDURE dbo.POS_ProductCombinations_insert
    @FK_ProductID INT,
    @FK_ProductItemID INT,
    @IsQuantified BIT,
    @Quantity DECIMAL (18, 4),
    @IsOptional BIT,
    @IsExtraCharge BIT,
    @DisplayOrder INT = NULL,
    @FK_CreatedUserID INT,
    @FK_UpdatedUserID INT = NULL,
    @DateCreated DATETIME,
    @DateUpdated DATETIME = NULL
AS
BEGIN
    DECLARE @Inserted TABLE (ProductCombinationID INT);

    INSERT INTO POS_ProductCombinations (FK_ProductID, FK_ProductItemID, IsQuantified, Quantity, IsOptional, IsExtraCharge, DisplayOrder, FK_CreatedUserID, FK_UpdatedUserID, DateCreated, DateUpdated)
    OUTPUT INSERTED.ProductCombinationID INTO @Inserted
    VALUES (@FK_ProductID, @FK_ProductItemID, @IsQuantified, @Quantity, @IsOptional, @IsExtraCharge, @DisplayOrder, @FK_CreatedUserID, @FK_UpdatedUserID, @DateCreated, @DateUpdated);

    SELECT *
    FROM POS_ProductCombinations
    WHERE ProductCombinationID = 
    (
        SELECT TOP 1 ProductCombinationID
        FROM @Inserted
    );
END
GO