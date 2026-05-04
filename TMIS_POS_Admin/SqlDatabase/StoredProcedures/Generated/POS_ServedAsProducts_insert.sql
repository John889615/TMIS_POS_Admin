USE [TMIS_Development]
GO

IF OBJECT_ID('dbo.POS_ServedAsProducts_insert', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_ServedAsProducts_insert;
GO

CREATE PROCEDURE dbo.POS_ServedAsProducts_insert
    @FK_ProductID INT,
    @FK_ServedAsID INT,
    @IsQuantified BIT,
    @Quantity DECIMAL (18, 4),
    @FK_CreatedUserID INT,
    @FK_UpdatedUserID INT = NULL,
    @DateCreated DATETIME,
    @DateUpdated DATETIME = NULL,
    @IsDefault BIT
AS
BEGIN
    DECLARE @Inserted TABLE (ServedAsProductID INT);

    INSERT INTO POS_ServedAsProducts (FK_ProductID, FK_ServedAsID, IsQuantified, Quantity, FK_CreatedUserID, FK_UpdatedUserID, DateCreated, DateUpdated, IsDefault)
    OUTPUT INSERTED.ServedAsProductID INTO @Inserted
    VALUES (@FK_ProductID, @FK_ServedAsID, @IsQuantified, @Quantity, @FK_CreatedUserID, @FK_UpdatedUserID, @DateCreated, @DateUpdated, @IsDefault);

    SELECT *
    FROM POS_ServedAsProducts
    WHERE ServedAsProductID = 
    (
        SELECT TOP 1 ServedAsProductID
        FROM @Inserted
    );
END
GO