USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_Units_update', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_Units_update;
GO

CREATE PROCEDURE dbo.POS_Units_update
    @UnitID INT,
    @Unit VARCHAR(255),
    @Symbol VARCHAR(10) = NULL,
    @BC_ID VARCHAR(255) = NULL,
    @IsActive BIT,
    @DateCreated DATETIME,
    @DateUpdated DATETIME = NULL
AS
BEGIN
    UPDATE POS_Units
    SET     Unit = @Unit,
    Symbol = @Symbol,
    BC_ID = @BC_ID,
    IsActive = @IsActive,
    DateUpdated = @DateUpdated
    WHERE UnitID = @UnitID;

    SELECT *
    FROM POS_Units
    WHERE UnitID = @UnitID;
END
GO