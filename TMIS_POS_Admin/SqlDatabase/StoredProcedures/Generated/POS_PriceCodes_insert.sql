USE [TMIS_Development]
GO

IF OBJECT_ID('dbo.POS_PriceCodes_insert', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_PriceCodes_insert;
GO

CREATE PROCEDURE dbo.POS_PriceCodes_insert
    @PriceCode VARCHAR(20),
    @Description VARCHAR(255) = NULL,
    @IsActive BIT,
    @DateCreated DATETIME,
    @DateUpdated DATETIME
AS
BEGIN
    DECLARE @Inserted TABLE (PriceCodeID INT);

    INSERT INTO POS_PriceCodes (PriceCode, Description, IsActive, DateCreated, DateUpdated)
    OUTPUT INSERTED.PriceCodeID INTO @Inserted
    VALUES (@PriceCode, @Description, @IsActive, @DateCreated, @DateUpdated);

    SELECT *
    FROM POS_PriceCodes
    WHERE PriceCodeID = 
    (
        SELECT TOP 1 PriceCodeID
        FROM @Inserted
    );
END
GO