USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_CostCenters_update', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_CostCenters_update;
GO

CREATE PROCEDURE dbo.POS_CostCenters_update
    @CostCenterID INT,
    @FK_LocationID INT,
    @Name VARCHAR(255),
    @BillingReference VARCHAR(255),
    @FK_StatusID INT,
    @FK_CostCenterTypeID INT,
    @DateCreated DATETIME,
    @DateUpdated DATETIME,
    @BC_ID VARCHAR(255) = NULL
AS
BEGIN
    UPDATE POS_CostCenters
    SET     FK_LocationID = @FK_LocationID,
    [Name] = @Name,
    BillingReference = @BillingReference,
    FK_StatusID = @FK_StatusID,
    FK_CostCenterTypeID = @FK_CostCenterTypeID,
    DateUpdated = @DateUpdated,
    BC_ID = @BC_ID
    WHERE CostCenterID = @CostCenterID;

    SELECT *
    FROM POS_CostCenters
    WHERE CostCenterID = @CostCenterID;
END
GO