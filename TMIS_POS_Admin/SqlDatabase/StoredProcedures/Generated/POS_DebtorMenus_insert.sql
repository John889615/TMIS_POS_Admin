USE [TMIS_Development]
GO

IF OBJECT_ID('dbo.POS_DebtorMenus_insert', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_DebtorMenus_insert;
GO

CREATE PROCEDURE dbo.POS_DebtorMenus_insert
    @FK_LocationID INT,
    @FK_CostCenterID INT = NULL,
    @FK_MenuID INT = NULL,
    @MenuName VARCHAR(50),
    @ValidFrom DATETIME = NULL,
    @ValidTo DATETIME = NULL,
    @IsActive BIT,
    @DateCreated DATETIME,
    @DateUpdated DATETIME = NULL
AS
BEGIN
    DECLARE @Inserted TABLE (DebtorMenuID INT);

    INSERT INTO POS_DebtorMenus (FK_LocationID, FK_CostCenterID, FK_MenuID, MenuName, ValidFrom, ValidTo, IsActive, DateCreated, DateUpdated)
    OUTPUT INSERTED.DebtorMenuID INTO @Inserted
    VALUES (@FK_LocationID, @FK_CostCenterID, @FK_MenuID, @MenuName, @ValidFrom, @ValidTo, @IsActive, @DateCreated, @DateUpdated);

    SELECT *
    FROM POS_DebtorMenus
    WHERE DebtorMenuID = 
    (
        SELECT TOP 1 DebtorMenuID
        FROM @Inserted
    );
END
GO