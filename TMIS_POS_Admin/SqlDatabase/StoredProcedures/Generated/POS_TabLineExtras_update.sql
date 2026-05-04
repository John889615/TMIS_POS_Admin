USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_TabLineExtras_update', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_TabLineExtras_update;
GO

CREATE PROCEDURE dbo.POS_TabLineExtras_update
    @TabLineExtraID UNIQUEIDENTIFIER,
    @FK_TabLineID UNIQUEIDENTIFIER,
    @FK_ProductID INT,
    @Product VARCHAR(255)
AS
BEGIN
    UPDATE POS_TabLineExtras
    SET     FK_TabLineID = @FK_TabLineID,
    FK_ProductID = @FK_ProductID,
    Product = @Product
    WHERE TabLineExtraID = @TabLineExtraID;

    SELECT *
    FROM POS_TabLineExtras
    WHERE TabLineExtraID = @TabLineExtraID;
END
GO