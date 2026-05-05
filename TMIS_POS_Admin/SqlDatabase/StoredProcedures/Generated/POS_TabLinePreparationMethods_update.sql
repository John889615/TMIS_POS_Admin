USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_TabLinePreparationMethods_update', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_TabLinePreparationMethods_update;
GO

CREATE PROCEDURE dbo.POS_TabLinePreparationMethods_update
    @TabLinePreparationMethodID UNIQUEIDENTIFIER,
    @FK_TabLineCombinationID UNIQUEIDENTIFIER,
    @FK_PreparationMethodID INT,
    @PreparationMethodName VARCHAR(255)
AS
BEGIN
    UPDATE POS_TabLinePreparationMethods
    SET     FK_TabLineCombinationID = @FK_TabLineCombinationID,
    FK_PreparationMethodID = @FK_PreparationMethodID,
    PreparationMethodName = @PreparationMethodName
    WHERE TabLinePreparationMethodID = @TabLinePreparationMethodID;

    SELECT *
    FROM POS_TabLinePreparationMethods
    WHERE TabLinePreparationMethodID = @TabLinePreparationMethodID;
END
GO