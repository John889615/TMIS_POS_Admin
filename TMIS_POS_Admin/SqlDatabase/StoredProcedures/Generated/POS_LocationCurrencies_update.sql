USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_LocationCurrencies_update', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_LocationCurrencies_update;
GO

CREATE PROCEDURE dbo.POS_LocationCurrencies_update
    @LocationCurrencyID INT,
    @FK_CurrencyID INT,
    @FK_LocationID INT,
    @IsActive BIT,
    @DateCreated DATETIME,
    @FK_CreatedUserID INT,
    @DateUpdated DATETIME = NULL,
    @FK_UpdatedUserID INT
AS
BEGIN
    UPDATE POS_LocationCurrencies
    SET     FK_CurrencyID = @FK_CurrencyID,
    FK_LocationID = @FK_LocationID,
    IsActive = @IsActive,
    FK_CreatedUserID = @FK_CreatedUserID,
    DateUpdated = @DateUpdated,
    FK_UpdatedUserID = @FK_UpdatedUserID
    WHERE LocationCurrencyID = @LocationCurrencyID;

    SELECT *
    FROM POS_LocationCurrencies
    WHERE LocationCurrencyID = @LocationCurrencyID;
END
GO