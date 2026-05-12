USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_StockRequests_update', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_StockRequests_update;
GO

CREATE PROCEDURE dbo.POS_StockRequests_update
    @StockRequestID INT,
    @RefNumber VARCHAR(50) = NULL,
    @FK_FromDebtorID INT,
    @FK_ToDebtorID INT,
    @FK_OrderStatusID INT,
    @FK_UserID INT,
    @ManagerNotes VARCHAR(255) = NULL,
    @Notes VARCHAR(MAX) = NULL,
    @DateOrdered DATETIME,
    @DateUpdated DATETIME = NULL,
    @FK_ApprovedByUserID INT = NULL,
    @DateApproved DATETIME = NULL
AS
BEGIN
    UPDATE POS_StockRequests
    SET     RefNumber = @RefNumber,
    FK_FromDebtorID = @FK_FromDebtorID,
    FK_ToDebtorID = @FK_ToDebtorID,
    FK_OrderStatusID = @FK_OrderStatusID,
    FK_UserID = @FK_UserID,
    ManagerNotes = @ManagerNotes,
    Notes = @Notes,
    DateOrdered = @DateOrdered,
    DateUpdated = @DateUpdated,
    FK_ApprovedByUserID = @FK_ApprovedByUserID,
    DateApproved = @DateApproved
    WHERE StockRequestID = @StockRequestID;

    SELECT *
    FROM POS_StockRequests
    WHERE StockRequestID = @StockRequestID;
END
GO