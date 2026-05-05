USE [TMIS_Development]
GO

IF OBJECT_ID('dbo.POS_TaxTypes_insert', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_TaxTypes_insert;
GO

CREATE PROCEDURE dbo.POS_TaxTypes_insert
    @TaxName VARCHAR(255),
    @TaxPercentage INT,
    @ValidFrom DATETIME = NULL,
    @ValidTo DATETIME = NULL,
    @IsActive BIT,
    @DateCreated DATETIME,
    @DateUpdated DATETIME
AS
BEGIN
    DECLARE @Inserted TABLE (TaxTypeID INT);

    INSERT INTO POS_TaxTypes (TaxName, TaxPercentage, ValidFrom, ValidTo, IsActive, DateCreated, DateUpdated)
    OUTPUT INSERTED.TaxTypeID INTO @Inserted
    VALUES (@TaxName, @TaxPercentage, @ValidFrom, @ValidTo, @IsActive, @DateCreated, @DateUpdated);

    SELECT *
    FROM POS_TaxTypes
    WHERE TaxTypeID = 
    (
        SELECT TOP 1 TaxTypeID
        FROM @Inserted
    );
END
GO