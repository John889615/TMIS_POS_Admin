USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_TaxTypes_update', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_TaxTypes_update;
GO

CREATE PROCEDURE dbo.POS_TaxTypes_update
    @TaxTypeID INT,
    @TaxName VARCHAR(255),
    @TaxPercentage INT,
    @ValidFrom DATETIME = NULL,
    @ValidTo DATETIME = NULL,
    @IsActive BIT,
    @DateCreated DATETIME,
    @DateUpdated DATETIME
AS
BEGIN
    UPDATE POS_TaxTypes
    SET     TaxName = @TaxName,
    TaxPercentage = @TaxPercentage,
    ValidFrom = @ValidFrom,
    ValidTo = @ValidTo,
    IsActive = @IsActive,
    DateUpdated = @DateUpdated
    WHERE TaxTypeID = @TaxTypeID;

    SELECT *
    FROM POS_TaxTypes
    WHERE TaxTypeID = @TaxTypeID;
END
GO