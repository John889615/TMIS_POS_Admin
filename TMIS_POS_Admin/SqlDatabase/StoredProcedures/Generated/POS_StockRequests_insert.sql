USE [TMIS_Development]
GO

IF OBJECT_ID('dbo.POS_StockRequests_insert', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_StockRequests_insert;
GO

CREATE PROCEDURE dbo.POS_StockRequests_insert
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
    DECLARE @Inserted TABLE (StockRequestID INT);

    INSERT INTO POS_StockRequests (RefNumber, FK_FromDebtorID, FK_ToDebtorID, FK_OrderStatusID, FK_UserID, ManagerNotes, Notes, DateOrdered, DateUpdated, FK_ApprovedByUserID, DateApproved)
    OUTPUT INSERTED.StockRequestID INTO @Inserted
    VALUES (@RefNumber, @FK_FromDebtorID, @FK_ToDebtorID, @FK_OrderStatusID, @FK_UserID, @ManagerNotes, @Notes, @DateOrdered, @DateUpdated, @FK_ApprovedByUserID, @DateApproved);

    SELECT *
    FROM POS_StockRequests
    WHERE StockRequestID = 
    (
        SELECT TOP 1 StockRequestID
        FROM @Inserted
    );
END
GO