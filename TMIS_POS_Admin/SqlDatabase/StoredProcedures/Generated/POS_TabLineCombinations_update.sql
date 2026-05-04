USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_TabLineCombinations_update', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_TabLineCombinations_update;
GO

CREATE PROCEDURE dbo.POS_TabLineCombinations_update
    @TabLineCombinationID UNIQUEIDENTIFIER,
    @FK_TabLineID UNIQUEIDENTIFIER,
    @FK_ProductCombinationID INT,
    @Product VARCHAR(255),
    @Hold BIT,
    @Notes VARCHAR(MAX) = NULL
AS
BEGIN
    UPDATE POS_TabLineCombinations
    SET     FK_TabLineID = @FK_TabLineID,
    FK_ProductCombinationID = @FK_ProductCombinationID,
    Product = @Product,
    Hold = @Hold,
    Notes = @Notes
    WHERE TabLineCombinationID = @TabLineCombinationID;

    SELECT *
    FROM POS_TabLineCombinations
    WHERE TabLineCombinationID = @TabLineCombinationID;
END
GO