USE [TMIS_Development]
GO

IF OBJECT_ID('dbo.POS_CostCenterTypes_insert', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_CostCenterTypes_insert;
GO

CREATE PROCEDURE dbo.POS_CostCenterTypes_insert
    @Name VARCHAR(50),
    @DateCreated DATETIME,
    @DateUpdated DATETIME = NULL
AS
BEGIN
    DECLARE @Inserted TABLE (CostCenterTypeID INT);

    INSERT INTO POS_CostCenterTypes ([Name], DateCreated, DateUpdated)
    OUTPUT INSERTED.CostCenterTypeID INTO @Inserted
    VALUES (@Name, @DateCreated, @DateUpdated);

    SELECT *
    FROM POS_CostCenterTypes
    WHERE CostCenterTypeID = 
    (
        SELECT TOP 1 CostCenterTypeID
        FROM @Inserted
    );
END
GO