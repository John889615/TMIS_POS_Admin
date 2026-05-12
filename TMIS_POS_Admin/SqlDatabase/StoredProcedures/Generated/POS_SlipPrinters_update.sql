USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_SlipPrinters_update', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_SlipPrinters_update;
GO

CREATE PROCEDURE dbo.POS_SlipPrinters_update
    @SlipPrinterID INT,
    @FK_LocationID INT,
    @CostCenterID INT = NULL,
    @Name VARCHAR(50),
    @Model VARCHAR(50),
    @IpAddress VARCHAR(20),
    @Port INT,
    @IsDefault BIT,
    @IsActive BIT,
    @FK_CreatedUserID INT,
    @FK_UpdatedUserID INT,
    @DateCreated DATETIME,
    @DateUpdated DATETIME = NULL,
    @AutoCut BIT = NULL
AS
BEGIN
    UPDATE POS_SlipPrinters
    SET     FK_LocationID = @FK_LocationID,
    CostCenterID = @CostCenterID,
    [Name] = @Name,
    Model = @Model,
    IpAddress = @IpAddress,
    Port = @Port,
    IsDefault = @IsDefault,
    IsActive = @IsActive,
    FK_CreatedUserID = @FK_CreatedUserID,
    FK_UpdatedUserID = @FK_UpdatedUserID,
    DateUpdated = @DateUpdated,
    AutoCut = @AutoCut
    WHERE SlipPrinterID = @SlipPrinterID;

    SELECT *
    FROM POS_SlipPrinters
    WHERE SlipPrinterID = @SlipPrinterID;
END
GO