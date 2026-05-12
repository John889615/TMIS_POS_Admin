USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_ProductPreparationMethods_update', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_ProductPreparationMethods_update;
GO

CREATE PROCEDURE dbo.POS_ProductPreparationMethods_update
    @ProductPreparationMethodID INT,
    @ShortCode VARCHAR(10),
    @Method VARCHAR(50),
    @FK_CreatedUserID INT,
    @FK_UpdatedUserID INT = NULL,
    @DateCreated DATETIME,
    @DateUpdated DATETIME = NULL
AS
BEGIN
    UPDATE POS_ProductPreparationMethods
    SET     ShortCode = @ShortCode,
    Method = @Method,
    FK_CreatedUserID = @FK_CreatedUserID,
    FK_UpdatedUserID = @FK_UpdatedUserID,
    DateUpdated = @DateUpdated
    WHERE ProductPreparationMethodID = @ProductPreparationMethodID;

    SELECT *
    FROM POS_ProductPreparationMethods
    WHERE ProductPreparationMethodID = @ProductPreparationMethodID;
END
GO