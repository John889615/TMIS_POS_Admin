USE [TMIS_Development]
GO

IF OBJECT_ID('dbo.DebtorTypeMappings_insert', 'P') IS NOT NULL
    DROP PROCEDURE dbo.DebtorTypeMappings_insert;
GO

CREATE PROCEDURE dbo.DebtorTypeMappings_insert
    @FK_DebtorID INT,
    @FK_DebtorTypeID INT,
    @FK_StatusID INT,
    @FK_BranchID INT = NULL,
    @FK_DepartmentID INT = NULL,
    @DateCreated DATETIME,
    @DateUpdated DATETIME
AS
BEGIN
    DECLARE @Inserted TABLE (DebtorTypeMappingID INT);

    INSERT INTO DebtorTypeMappings (FK_DebtorID, FK_DebtorTypeID, FK_StatusID, FK_BranchID, FK_DepartmentID, DateCreated, DateUpdated)
    OUTPUT INSERTED.DebtorTypeMappingID INTO @Inserted
    VALUES (@FK_DebtorID, @FK_DebtorTypeID, @FK_StatusID, @FK_BranchID, @FK_DepartmentID, @DateCreated, @DateUpdated);

    SELECT *
    FROM DebtorTypeMappings
    WHERE DebtorTypeMappingID = 
    (
        SELECT TOP 1 DebtorTypeMappingID
        FROM @Inserted
    );
END
GO