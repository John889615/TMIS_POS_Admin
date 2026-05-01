USE [TMIS_Development]
GO

IF OBJECT_ID('dbo.POS_DebtorProductPriceHistory_insert', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_DebtorProductPriceHistory_insert;
GO

CREATE PROCEDURE dbo.POS_DebtorProductPriceHistory_insert
    @FK_DebtorProductID INT,
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
    DECLARE @Inserted TABLE (DebtorProductPriceHistoryID INT);

    INSERT INTO POS_DebtorProductPriceHistory (FK_DebtorProductID, [Value], Vat, ItemPrice, ValidFrom, ValidTo, FK_CreatedUserID, FK_UpdatedUserID, DateCreated, DateUpdated)
    OUTPUT INSERTED.DebtorProductPriceHistoryID INTO @Inserted
    VALUES (@FK_DebtorProductID, @Value, @Vat, @ItemPrice, @ValidFrom, @ValidTo, @FK_CreatedUserID, @FK_UpdatedUserID, @DateCreated, @DateUpdated);

    SELECT *
    FROM POS_DebtorProductPriceHistory
    WHERE DebtorProductPriceHistoryID = 
    (
        SELECT TOP 1 DebtorProductPriceHistoryID
        FROM @Inserted
    );
END
GO