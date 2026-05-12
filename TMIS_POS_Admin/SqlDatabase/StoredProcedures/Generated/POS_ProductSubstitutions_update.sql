USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_ProductSubstitutions_update', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_ProductSubstitutions_update;
GO

CREATE PROCEDURE dbo.POS_ProductSubstitutions_update
    @ProductSubstitutionID INT,
    @FK_ProductID INT,
    @FK_ProductSubstitutionID INT,
    @IsQuantified BIT,
    @Quantity DECIMAL (18, 4),
    @IsExtraCharge BIT,
    @FK_CreatedUserID INT,
    @FK_UpdatedUserID INT = NULL,
    @DateCreated DATETIME,
    @DateUpdated DATETIME = NULL
AS
BEGIN
    UPDATE POS_ProductSubstitutions
    SET     FK_ProductID = @FK_ProductID,
    FK_ProductSubstitutionID = @FK_ProductSubstitutionID,
    IsQuantified = @IsQuantified,
    Quantity = @Quantity,
    IsExtraCharge = @IsExtraCharge,
    FK_CreatedUserID = @FK_CreatedUserID,
    FK_UpdatedUserID = @FK_UpdatedUserID,
    DateUpdated = @DateUpdated
    WHERE ProductSubstitutionID = @ProductSubstitutionID;

    SELECT *
    FROM POS_ProductSubstitutions
    WHERE ProductSubstitutionID = @ProductSubstitutionID;
END
GO