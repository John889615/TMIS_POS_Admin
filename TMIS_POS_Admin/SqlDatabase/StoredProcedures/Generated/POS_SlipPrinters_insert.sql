USE [TMIS_Development]
GO

IF OBJECT_ID('dbo.POS_SlipPrinters_insert', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_SlipPrinters_insert;
GO

CREATE PROCEDURE dbo.POS_SlipPrinters_insert
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
    @DateUpdated DATETIME,
    @AutoCut BIT = NULL
AS
BEGIN
    DECLARE @Inserted TABLE (SlipPrinterID INT);

    INSERT INTO POS_SlipPrinters (FK_LocationID, CostCenterID, [Name], Model, IpAddress, Port, IsDefault, IsActive, FK_CreatedUserID, FK_UpdatedUserID, DateCreated, DateUpdated, AutoCut)
    OUTPUT INSERTED.SlipPrinterID INTO @Inserted
    VALUES (@FK_LocationID, @CostCenterID, @Name, @Model, @IpAddress, @Port, @IsDefault, @IsActive, @FK_CreatedUserID, @FK_UpdatedUserID, @DateCreated, @DateUpdated, @AutoCut);

    SELECT *
    FROM POS_SlipPrinters
    WHERE SlipPrinterID = 
    (
        SELECT TOP 1 SlipPrinterID
        FROM @Inserted
    );
END
GO