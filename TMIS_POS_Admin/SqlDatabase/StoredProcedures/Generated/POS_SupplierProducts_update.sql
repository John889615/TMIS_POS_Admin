USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_SupplierProducts_update', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_SupplierProducts_update;
GO

CREATE PROCEDURE dbo.POS_SupplierProducts_update
    @SupplierProductID INT,
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
    @DateUpdated DATETIME
AS
BEGIN
    UPDATE POS_SupplierProducts
    SET     FK_CreditorID = @FK_CreditorID,
    FK_ProductID = @FK_ProductID,
    FK_DebtorID = @FK_DebtorID,
    SupplierItemCode = @SupplierItemCode,
    FK_BaseUnitID = @FK_BaseUnitID,
    FK_PacUnitID = @FK_PacUnitID,
    UnitsPerPack = @UnitsPerPack,
    Quantity = @Quantity,
    TrackPackLevel = @TrackPackLevel,
    LastPurchasePrice = @LastPurchasePrice,
    LastPurchaseDate = @LastPurchaseDate,
    FK_TaxTypeID = @FK_TaxTypeID,
    LeadTimeDays = @LeadTimeDays,
    IsPreferred = @IsPreferred,
    IsActive = @IsActive,
    DateAdded = @DateAdded,
    DateUpdated = @DateUpdated
    WHERE SupplierProductID = @SupplierProductID;

    SELECT *
    FROM POS_SupplierProducts
    WHERE SupplierProductID = @SupplierProductID;
END
GO