USE [TMIS_Development]
GO

IF OBJECT_ID('dbo.POS_ProductSubstitutions_insert', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_ProductSubstitutions_insert;
GO

CREATE PROCEDURE dbo.POS_ProductSubstitutions_insert
    @FK_ProductID INT,
    @FK_ProductSubstitutionID INT,
    @IsQuantified BIT,
    @Quantity DECIMAL (18, 4),
    @IsExtraCharge BIT,
    @FK_CreatedUserID INT,
    @FK_UpdatedUserID INT = NULL,
    @DateCreated DATETIME,
    @DateUpdated DATETIME = NULL
AS
BEGIN
    DECLARE @Inserted TABLE (ProductSubstitutionID INT);

    INSERT INTO POS_ProductSubstitutions (FK_ProductID, FK_ProductSubstitutionID, IsQuantified, Quantity, IsExtraCharge, FK_CreatedUserID, FK_UpdatedUserID, DateCreated, DateUpdated)
    OUTPUT INSERTED.ProductSubstitutionID INTO @Inserted
    VALUES (@FK_ProductID, @FK_ProductSubstitutionID, @IsQuantified, @Quantity, @IsExtraCharge, @FK_CreatedUserID, @FK_UpdatedUserID, @DateCreated, @DateUpdated);

    SELECT *
    FROM POS_ProductSubstitutions
    WHERE ProductSubstitutionID = 
    (
        SELECT TOP 1 ProductSubstitutionID
        FROM @Inserted
    );
END
GO