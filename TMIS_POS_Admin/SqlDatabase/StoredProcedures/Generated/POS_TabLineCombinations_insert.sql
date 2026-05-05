USE [TMIS_Development]
GO

IF OBJECT_ID('dbo.POS_TabLineCombinations_insert', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_TabLineCombinations_insert;
GO

CREATE PROCEDURE dbo.POS_TabLineCombinations_insert
    @FK_TabLineID UNIQUEIDENTIFIER,
    @FK_ProductCombinationID INT,
    @Product VARCHAR(255),
    @Hold BIT,
    @Notes VARCHAR(MAX) = NULL
AS
BEGIN
    DECLARE @Inserted TABLE (TabLineCombinationID UNIQUEIDENTIFIER);

    INSERT INTO POS_TabLineCombinations (FK_TabLineID, FK_ProductCombinationID, Product, Hold, Notes)
    OUTPUT INSERTED.TabLineCombinationID INTO @Inserted
    VALUES (@FK_TabLineID, @FK_ProductCombinationID, @Product, @Hold, @Notes);

    SELECT *
    FROM POS_TabLineCombinations
    WHERE TabLineCombinationID = 
    (
        SELECT TOP 1 TabLineCombinationID
        FROM @Inserted
    );
END
GO