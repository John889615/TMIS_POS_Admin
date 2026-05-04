USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_CostCenterTypes_update', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_CostCenterTypes_update;
GO

CREATE PROCEDURE dbo.POS_CostCenterTypes_update
    @CostCenterTypeID INT,
    @Name VARCHAR(50),
    @DateCreated DATETIME,
    @DateUpdated DATETIME
AS
BEGIN
    UPDATE POS_CostCenterTypes
    SET     [Name] = @Name,
    DateUpdated = @DateUpdated
    WHERE CostCenterTypeID = @CostCenterTypeID;

    SELECT *
    FROM POS_CostCenterTypes
    WHERE CostCenterTypeID = @CostCenterTypeID;
END
GO