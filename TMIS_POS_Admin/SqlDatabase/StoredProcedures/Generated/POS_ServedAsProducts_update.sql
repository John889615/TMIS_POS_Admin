USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_ServedAsProducts_update', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_ServedAsProducts_update;
GO

CREATE PROCEDURE dbo.POS_ServedAsProducts_update
    @ServedAsProductID INT,
    @FK_ProductID INT,
    @FK_ServedAsID INT,
    @IsQuantified BIT,
    @Quantity DECIMAL (18, 4),
    @FK_CreatedUserID INT,
    @FK_UpdatedUserID INT = NULL,
    @DateCreated DATETIME,
    @DateUpdated DATETIME = NULL,
    @IsDefault BIT
AS
BEGIN
    UPDATE POS_ServedAsProducts
    SET     FK_ProductID = @FK_ProductID,
    FK_ServedAsID = @FK_ServedAsID,
    IsQuantified = @IsQuantified,
    Quantity = @Quantity,
    FK_CreatedUserID = @FK_CreatedUserID,
    FK_UpdatedUserID = @FK_UpdatedUserID,
    DateUpdated = @DateUpdated,
    IsDefault = @IsDefault
    WHERE ServedAsProductID = @ServedAsProductID;

    SELECT *
    FROM POS_ServedAsProducts
    WHERE ServedAsProductID = @ServedAsProductID;
END
GO