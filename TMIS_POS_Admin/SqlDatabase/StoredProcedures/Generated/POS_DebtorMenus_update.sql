USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_DebtorMenus_update', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_DebtorMenus_update;
GO

CREATE PROCEDURE dbo.POS_DebtorMenus_update
    @DebtorMenuID INT,
    @FK_LocationID INT,
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
    UPDATE POS_DebtorMenus
    SET     FK_LocationID = @FK_LocationID,
    FK_CostCenterID = @FK_CostCenterID,
    FK_MenuID = @FK_MenuID,
    MenuName = @MenuName,
    ValidFrom = @ValidFrom,
    ValidTo = @ValidTo,
    IsActive = @IsActive,
    DateUpdated = @DateUpdated
    WHERE DebtorMenuID = @DebtorMenuID;

    SELECT *
    FROM POS_DebtorMenus
    WHERE DebtorMenuID = @DebtorMenuID;
END
GO