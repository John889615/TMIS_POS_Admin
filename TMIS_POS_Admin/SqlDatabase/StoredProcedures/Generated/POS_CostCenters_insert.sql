USE [TMIS_Development]
GO

IF OBJECT_ID('dbo.POS_CostCenters_insert', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_CostCenters_insert;
GO

CREATE PROCEDURE dbo.POS_CostCenters_insert
    @FK_LocationID INT,
    @Name VARCHAR(255),
    @BillingReference VARCHAR(255),
    @FK_StatusID INT,
    @FK_CostCenterTypeID INT,
    @DateCreated DATETIME,
    @DateUpdated DATETIME = NULL,
    @BC_ID VARCHAR(255) = NULL
AS
BEGIN
    DECLARE @Inserted TABLE (CostCenterID INT);

    INSERT INTO POS_CostCenters (FK_LocationID, [Name], BillingReference, FK_StatusID, FK_CostCenterTypeID, DateCreated, DateUpdated, BC_ID)
    OUTPUT INSERTED.CostCenterID INTO @Inserted
    VALUES (@FK_LocationID, @Name, @BillingReference, @FK_StatusID, @FK_CostCenterTypeID, @DateCreated, @DateUpdated, @BC_ID);

    SELECT *
    FROM POS_CostCenters
    WHERE CostCenterID = 
    (
        SELECT TOP 1 CostCenterID
        FROM @Inserted
    );
END
GO