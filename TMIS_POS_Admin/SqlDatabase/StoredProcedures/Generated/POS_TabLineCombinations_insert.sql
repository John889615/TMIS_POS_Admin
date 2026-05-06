USE [TMIS_Development]
GO

IF OBJECT_ID('dbo.POS_TabLineCombinations_insert', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_TabLineCombinations_insert;
GO

CREATE PROCEDURE dbo.POS_TabLineCombinations_insert
    @TabLineCombinationID UNIQUEIDENTIFIER = NULL,
    @FK_TabLineID UNIQUEIDENTIFIER,
    @FK_ProductCombinationID INT,
    @Product VARCHAR(255),
    @Hold BIT,
    @Notes VARCHAR(MAX) = NULL
AS
BEGIN
    DECLARE @Inserted TABLE (TabLineCombinationID UNIQUEIDENTIFIER);

    INSERT INTO POS_TabLineCombinations (TabLineCombinationID, FK_TabLineID, FK_ProductCombinationID, Product, Hold, Notes)
    OUTPUT INSERTED.TabLineCombinationID INTO @Inserted
    VALUES (ISNULL(@TabLineCombinationID, NEWID()), @FK_TabLineID, @FK_ProductCombinationID, @Product, @Hold, @Notes);

    SELECT *
    FROM POS_TabLineCombinations
    WHERE TabLineCombinationID = 
    (
        SELECT TOP 1 TabLineCombinationID
        FROM @Inserted
    );
END
GO