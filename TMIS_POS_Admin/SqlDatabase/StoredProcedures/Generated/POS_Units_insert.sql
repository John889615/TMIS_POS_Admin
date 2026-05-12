USE [TMIS_Development]
GO

IF OBJECT_ID('dbo.POS_Units_insert', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_Units_insert;
GO

CREATE PROCEDURE dbo.POS_Units_insert
    @Unit VARCHAR(255),
    @Symbol VARCHAR(10) = NULL,
    @BC_ID VARCHAR(255) = NULL,
    @IsActive BIT,
    @DateCreated DATETIME,
    @DateUpdated DATETIME = NULL
AS
BEGIN
    DECLARE @Inserted TABLE (UnitID INT);

    INSERT INTO POS_Units (Unit, Symbol, BC_ID, IsActive, DateCreated, DateUpdated)
    OUTPUT INSERTED.UnitID INTO @Inserted
    VALUES (@Unit, @Symbol, @BC_ID, @IsActive, @DateCreated, @DateUpdated);

    SELECT *
    FROM POS_Units
    WHERE UnitID = 
    (
        SELECT TOP 1 UnitID
        FROM @Inserted
    );
END
GO