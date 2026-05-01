USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_StockRequestLines_update', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_StockRequestLines_update;
GO

CREATE PROCEDURE dbo.POS_StockRequestLines_update
    @StockRequestLineID INT,
    @FK_StockRequestID INT,
    @FK_ProductID INT,
    @Quantity DECIMAL (18, 4),
    @Notes VARCHAR(255) = NULL,
    @ManagerNotes VARCHAR(255) = NULL,
    @IsDeclined BIT,
    @ApprovedQuantity DECIMAL (18, 4) = NULL
AS
BEGIN
    UPDATE POS_StockRequestLines
    SET     FK_StockRequestID = @FK_StockRequestID,
    FK_ProductID = @FK_ProductID,
    Quantity = @Quantity,
    Notes = @Notes,
    ManagerNotes = @ManagerNotes,
    IsDeclined = @IsDeclined,
    ApprovedQuantity = @ApprovedQuantity
    WHERE StockRequestLineID = @StockRequestLineID;

    SELECT *
    FROM POS_StockRequestLines
    WHERE StockRequestLineID = @StockRequestLineID;
END
GO