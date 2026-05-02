USE [TMIS_Development]
GO

IF OBJECT_ID('dbo.POS_StockRequestLines_insert', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_StockRequestLines_insert;
GO

CREATE PROCEDURE dbo.POS_StockRequestLines_insert
    @FK_StockRequestID INT,
    @FK_ProductID INT,
    @Quantity DECIMAL (18, 4),
    @Notes VARCHAR(255) = NULL,
    @ManagerNotes VARCHAR(255) = NULL,
    @IsDeclined BIT,
    @ApprovedQuantity DECIMAL (18, 4) = NULL,
    @FK_UnitID INT = NULL
AS
BEGIN
    DECLARE @Inserted TABLE (StockRequestLineID INT);

    INSERT INTO POS_StockRequestLines (FK_StockRequestID, FK_ProductID, Quantity, Notes, ManagerNotes, IsDeclined, ApprovedQuantity, FK_UnitID)
    OUTPUT INSERTED.StockRequestLineID INTO @Inserted
    VALUES (@FK_StockRequestID, @FK_ProductID, @Quantity, @Notes, @ManagerNotes, @IsDeclined, @ApprovedQuantity, @FK_UnitID);

    SELECT *
    FROM POS_StockRequestLines
    WHERE StockRequestLineID = 
    (
        SELECT TOP 1 StockRequestLineID
        FROM @Inserted
    );
END
GO