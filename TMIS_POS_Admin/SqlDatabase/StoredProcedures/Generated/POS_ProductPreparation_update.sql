USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_ProductPreparation_update', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_ProductPreparation_update;
GO

CREATE PROCEDURE dbo.POS_ProductPreparation_update
    @ProductPreparationID INT,
    @FK_ProductID INT,
    @FK_ProductPreparationMethodID INT,
    @FK_CreatedUserID INT,
    @FK_UpdatedUserID INT = NULL,
    @DateCreated DATETIME,
    @DateUpdated DATETIME = NULL
AS
BEGIN
    UPDATE POS_ProductPreparation
    SET     FK_ProductID = @FK_ProductID,
    FK_ProductPreparationMethodID = @FK_ProductPreparationMethodID,
    FK_CreatedUserID = @FK_CreatedUserID,
    FK_UpdatedUserID = @FK_UpdatedUserID,
    DateUpdated = @DateUpdated
    WHERE ProductPreparationID = @ProductPreparationID;

    SELECT *
    FROM POS_ProductPreparation
    WHERE ProductPreparationID = @ProductPreparationID;
END
GO