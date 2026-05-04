USE [TMIS_Development]
GO

IF OBJECT_ID('dbo.DebtorTypes_insert', 'P') IS NOT NULL
    DROP PROCEDURE dbo.DebtorTypes_insert;
GO

CREATE PROCEDURE dbo.DebtorTypes_insert
    @Type VARCHAR(50),
    @Description VARCHAR(255),
    @DateCreated DATETIME,
    @DateUpdated DATETIME
AS
BEGIN
    DECLARE @Inserted TABLE (DebtorTypeID INT);

    INSERT INTO DebtorTypes ([Type], Description, DateCreated, DateUpdated)
    OUTPUT INSERTED.DebtorTypeID INTO @Inserted
    VALUES (@Type, @Description, @DateCreated, @DateUpdated);

    SELECT *
    FROM DebtorTypes
    WHERE DebtorTypeID = 
    (
        SELECT TOP 1 DebtorTypeID
        FROM @Inserted
    );
END
GO