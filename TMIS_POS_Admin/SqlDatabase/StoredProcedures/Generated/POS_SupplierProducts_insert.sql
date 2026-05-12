USE [TMIS_Development]
GO

IF OBJECT_ID('dbo.POS_SupplierProducts_insert', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_SupplierProducts_insert;
GO

CREATE PROCEDURE dbo.POS_SupplierProducts_insert
    @FK_CreditorID INT,
    @FK_ProductID INT,
    @FK_DebtorID INT,
    @SupplierItemCode VARCHAR(255),
    @FK_BaseUnitID INT,
    @FK_PacUnitID INT = NULL,
    @UnitsPerPack DECIMAL (18, 4) = NULL,
    @Quantity DECIMAL (18, 4),
    @TrackPackLevel BIT,
    @LastPurchasePrice DECIMAL (18, 4) = NULL,
    @LastPurchaseDate DATETIME = NULL,
    @FK_TaxTypeID INT,
    @LeadTimeDays INT = NULL,
    @IsPreferred INT,
    @IsActive BIT,
    @DateAdded DATETIME,
    @DateUpdated DATETIME = NULL
AS
BEGIN
    DECLARE @Inserted TABLE (SupplierProductID INT);

    INSERT INTO POS_SupplierProducts (FK_CreditorID, FK_ProductID, FK_DebtorID, SupplierItemCode, FK_BaseUnitID, FK_PacUnitID, UnitsPerPack, Quantity, TrackPackLevel, LastPurchasePrice, LastPurchaseDate, FK_TaxTypeID, LeadTimeDays, IsPreferred, IsActive, DateAdded, DateUpdated)
    OUTPUT INSERTED.SupplierProductID INTO @Inserted
    VALUES (@FK_CreditorID, @FK_ProductID, @FK_DebtorID, @SupplierItemCode, @FK_BaseUnitID, @FK_PacUnitID, @UnitsPerPack, @Quantity, @TrackPackLevel, @LastPurchasePrice, @LastPurchaseDate, @FK_TaxTypeID, @LeadTimeDays, @IsPreferred, @IsActive, @DateAdded, @DateUpdated);

    SELECT *
    FROM POS_SupplierProducts
    WHERE SupplierProductID = 
    (
        SELECT TOP 1 SupplierProductID
        FROM @Inserted
    );
END
GO