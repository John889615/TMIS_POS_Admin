USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_TabLinePreparationMethods_select_single', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_TabLinePreparationMethods_select_single;
GO

CREATE PROCEDURE dbo.POS_TabLinePreparationMethods_select_single
    @TabLinePreparationMethodID UNIQUEIDENTIFIER
AS
BEGIN
    SELECT *
    FROM POS_TabLinePreparationMethods
    WHERE TabLinePreparationMethodID = @TabLinePreparationMethodID;
END
GO