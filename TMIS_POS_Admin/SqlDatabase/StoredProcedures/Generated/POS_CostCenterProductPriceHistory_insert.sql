USE [TMIS_Development]
GO

IF OBJECT_ID('dbo.POS_CostCenterProductPriceHistory_insert', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_CostCenterProductPriceHistory_insert;
GO

CREATE PROCEDURE dbo.POS_CostCenterProductPriceHistory_insert
    @FK_CostCenterProductID INT,
    @Value DECIMAL (18, 4),
    @Vat DECIMAL (18, 4),
    @ItemPrice DECIMAL (18, 4),
    @ValidFrom DATETIME,
    @ValidTo DATETIME = NULL,
    @FK_CreatedUserID INT,
    @FK_UpdatedUserID INT = NULL,
    @DateCreated DATETIME,
    @DateUpdated DATETIME = NULL
AS
BEGIN
    DECLARE @Inserted TABLE (CostcenterProductPriceHistoryID INT);

    INSERT INTO POS_CostCenterProductPriceHistory (FK_CostCenterProductID, [Value], Vat, ItemPrice, ValidFrom, ValidTo, FK_CreatedUserID, FK_UpdatedUserID, DateCreated, DateUpdated)
    OUTPUT INSERTED.CostcenterProductPriceHistoryID INTO @Inserted
    VALUES (@FK_CostCenterProductID, @Value, @Vat, @ItemPrice, @ValidFrom, @ValidTo, @FK_CreatedUserID, @FK_UpdatedUserID, @DateCreated, @DateUpdated);

    SELECT *
    FROM POS_CostCenterProductPriceHistory
    WHERE CostcenterProductPriceHistoryID = 
    (
        SELECT TOP 1 CostcenterProductPriceHistoryID
        FROM @Inserted
    );
END
GO