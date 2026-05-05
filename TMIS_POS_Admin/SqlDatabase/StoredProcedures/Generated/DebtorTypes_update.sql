USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.DebtorTypes_update', 'P') IS NOT NULL
    DROP PROCEDURE dbo.DebtorTypes_update;
GO

CREATE PROCEDURE dbo.DebtorTypes_update
    @DebtorTypeID INT,
    @Type VARCHAR(50),
    @Description VARCHAR(255),
    @DateCreated DATETIME,
    @DateUpdated DATETIME
AS
BEGIN
    UPDATE DebtorTypes
    SET     [Type] = @Type,
    Description = @Description,
    DateUpdated = @DateUpdated
    WHERE DebtorTypeID = @DebtorTypeID;

    SELECT *
    FROM DebtorTypes
    WHERE DebtorTypeID = @DebtorTypeID;
END
GO