USE [TMIS_Development]
GO

IF OBJECT_ID('dbo.DebtorMenus_insert_custom', 'P') IS NOT NULL
    DROP PROCEDURE dbo.DebtorMenus_insert_custom;
GO

CREATE PROCEDURE dbo.DebtorMenus_insert_custom
    @POS_DebtorMenuID INT,
	@FK_DebtorID INT,
    @FK_CostCenterID INT = NULL,
    @FK_MenuID INT = NULL,
    @MenuName VARCHAR(50),
    @ValidFrom DATETIME = NULL,
    @ValidTo DATETIME = NULL,
    @IsActive BIT,
    @DateCreated DATETIME,
    @DateUpdated DATETIME
AS
BEGIN
	SET IDENTITY_INSERT POS_DebtorMenus ON;

    INSERT INTO POS_DebtorMenus (POS_DebtorMenuID, FK_DebtorID, FK_CostCenterID, FK_MenuID, MenuName, ValidFrom, ValidTo, IsActive, DateCreated, DateUpdated)
    VALUES (@POS_DebtorMenuID, @FK_DebtorID, @FK_CostCenterID, @FK_MenuID, @MenuName, @ValidFrom, @ValidTo, @IsActive, @DateCreated, @DateUpdated)
    
    SET IDENTITY_INSERT POS_DebtorMenus OFF;

	SELECT * FROM POS_DebtorMenus
    WHERE POS_DebtorMenuID = @POS_DebtorMenuID;
END
GO