USE [TMIS_Development]
GO

IF OBJECT_ID('dbo.POS_ProductPreparationMethods_insert', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_ProductPreparationMethods_insert;
GO

CREATE PROCEDURE dbo.POS_ProductPreparationMethods_insert
    @ShortCode VARCHAR(10),
    @Method VARCHAR(50),
    @FK_CreatedUserID INT,
    @FK_UpdatedUserID INT = NULL,
    @DateCreated DATETIME,
    @DateUpdated DATETIME
AS
BEGIN
    DECLARE @Inserted TABLE (ProductPreparationMethodID INT);

    INSERT INTO POS_ProductPreparationMethods (ShortCode, Method, FK_CreatedUserID, FK_UpdatedUserID, DateCreated, DateUpdated)
    OUTPUT INSERTED.ProductPreparationMethodID INTO @Inserted
    VALUES (@ShortCode, @Method, @FK_CreatedUserID, @FK_UpdatedUserID, @DateCreated, @DateUpdated);

    SELECT *
    FROM POS_ProductPreparationMethods
    WHERE ProductPreparationMethodID = 
    (
        SELECT TOP 1 ProductPreparationMethodID
        FROM @Inserted
    );
END
GO