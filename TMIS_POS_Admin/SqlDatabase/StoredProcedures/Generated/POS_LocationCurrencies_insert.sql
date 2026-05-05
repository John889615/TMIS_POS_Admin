USE [TMIS_Development]
GO

IF OBJECT_ID('dbo.POS_LocationCurrencies_insert', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_LocationCurrencies_insert;
GO

CREATE PROCEDURE dbo.POS_LocationCurrencies_insert
    @FK_CurrencyID INT,
    @FK_LocationID INT,
    @IsActive BIT,
    @DateCreated DATETIME,
    @FK_CreatedUserID INT,
    @DateUpdated DATETIME,
    @FK_UpdatedUserID INT
AS
BEGIN
    DECLARE @Inserted TABLE (LocationCurrencyID INT);

    INSERT INTO POS_LocationCurrencies (FK_CurrencyID, FK_LocationID, IsActive, DateCreated, FK_CreatedUserID, DateUpdated, FK_UpdatedUserID)
    OUTPUT INSERTED.LocationCurrencyID INTO @Inserted
    VALUES (@FK_CurrencyID, @FK_LocationID, @IsActive, @DateCreated, @FK_CreatedUserID, @DateUpdated, @FK_UpdatedUserID);

    SELECT *
    FROM POS_LocationCurrencies
    WHERE LocationCurrencyID = 
    (
        SELECT TOP 1 LocationCurrencyID
        FROM @Inserted
    );
END
GO