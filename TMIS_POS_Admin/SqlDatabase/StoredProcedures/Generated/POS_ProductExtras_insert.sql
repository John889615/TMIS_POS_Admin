USE [TMIS_Development]
GO

IF OBJECT_ID('dbo.POS_ProductExtras_insert', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_ProductExtras_insert;
GO

CREATE PROCEDURE dbo.POS_ProductExtras_insert
    @FK_ProductID INT,
    @FK_ProductExtraCategoryID INT,
    @FK_ProductExtraID INT,
    @IsQuantified BIT,
    @Quantity DECIMAL (18, 4),
    @IsExtraCharge BIT,
    @DisplayOrder INT = NULL,
    @FK_CreatedUserID INT,
    @FK_UpdatedUserID INT = NULL,
    @DateCreated DATETIME,
    @DateUpdated DATETIME
AS
BEGIN
    DECLARE @Inserted TABLE (ProductExtraID INT);

    INSERT INTO POS_ProductExtras (FK_ProductID, FK_ProductExtraCategoryID, FK_ProductExtraID, IsQuantified, Quantity, IsExtraCharge, DisplayOrder, FK_CreatedUserID, FK_UpdatedUserID, DateCreated, DateUpdated)
    OUTPUT INSERTED.ProductExtraID INTO @Inserted
    VALUES (@FK_ProductID, @FK_ProductExtraCategoryID, @FK_ProductExtraID, @IsQuantified, @Quantity, @IsExtraCharge, @DisplayOrder, @FK_CreatedUserID, @FK_UpdatedUserID, @DateCreated, @DateUpdated);

    SELECT *
    FROM POS_ProductExtras
    WHERE ProductExtraID = 
    (
        SELECT TOP 1 ProductExtraID
        FROM @Inserted
    );
END
GO