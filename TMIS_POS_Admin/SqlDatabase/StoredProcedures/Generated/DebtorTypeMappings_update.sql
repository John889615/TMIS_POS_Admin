USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.DebtorTypeMappings_update', 'P') IS NOT NULL
    DROP PROCEDURE dbo.DebtorTypeMappings_update;
GO

CREATE PROCEDURE dbo.DebtorTypeMappings_update
    @DebtorTypeMappingID INT,
    @FK_DebtorID INT,
    @FK_DebtorTypeID INT,
    @FK_StatusID INT,
    @FK_BranchID INT = NULL,
    @FK_DepartmentID INT = NULL,
    @DateCreated DATETIME,
    @DateUpdated DATETIME
AS
BEGIN
    UPDATE DebtorTypeMappings
    SET     FK_DebtorID = @FK_DebtorID,
    FK_DebtorTypeID = @FK_DebtorTypeID,
    FK_StatusID = @FK_StatusID,
    FK_BranchID = @FK_BranchID,
    FK_DepartmentID = @FK_DepartmentID,
    DateUpdated = @DateUpdated
    WHERE DebtorTypeMappingID = @DebtorTypeMappingID;

    SELECT *
    FROM DebtorTypeMappings
    WHERE DebtorTypeMappingID = @DebtorTypeMappingID;
END
GO