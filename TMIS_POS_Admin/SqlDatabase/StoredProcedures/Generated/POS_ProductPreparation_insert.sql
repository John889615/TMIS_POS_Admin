USE [TMIS_Development]
GO

IF OBJECT_ID('dbo.POS_ProductPreparation_insert', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_ProductPreparation_insert;
GO

CREATE PROCEDURE dbo.POS_ProductPreparation_insert
    @FK_ProductID INT,
    @FK_ProductPreparationMethodID INT,
    @FK_CreatedUserID INT,
    @FK_UpdatedUserID INT = NULL,
    @DateCreated DATETIME,
    @DateUpdated DATETIME = NULL
AS
BEGIN
    DECLARE @Inserted TABLE (ProductPreparationID INT);

    INSERT INTO POS_ProductPreparation (FK_ProductID, FK_ProductPreparationMethodID, FK_CreatedUserID, FK_UpdatedUserID, DateCreated, DateUpdated)
    OUTPUT INSERTED.ProductPreparationID INTO @Inserted
    VALUES (@FK_ProductID, @FK_ProductPreparationMethodID, @FK_CreatedUserID, @FK_UpdatedUserID, @DateCreated, @DateUpdated);

    SELECT *
    FROM POS_ProductPreparation
    WHERE ProductPreparationID = 
    (
        SELECT TOP 1 ProductPreparationID
        FROM @Inserted
    );
END
GO