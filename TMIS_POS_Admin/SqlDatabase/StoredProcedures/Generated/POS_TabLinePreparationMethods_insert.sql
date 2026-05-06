USE [TMIS_Development]
GO

IF OBJECT_ID('dbo.POS_TabLinePreparationMethods_insert', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_TabLinePreparationMethods_insert;
GO

CREATE PROCEDURE dbo.POS_TabLinePreparationMethods_insert
    @TabLinePreparationMethodID UNIQUEIDENTIFIER = NULL,
    @FK_TabLineCombinationID UNIQUEIDENTIFIER,
    @FK_PreparationMethodID INT,
    @PreparationMethodName VARCHAR(255)
AS
BEGIN
    DECLARE @Inserted TABLE (TabLinePreparationMethodID UNIQUEIDENTIFIER);

    INSERT INTO POS_TabLinePreparationMethods (TabLinePreparationMethodID, FK_TabLineCombinationID, FK_PreparationMethodID, PreparationMethodName)
    OUTPUT INSERTED.TabLinePreparationMethodID INTO @Inserted
    VALUES (ISNULL(@TabLinePreparationMethodID, NEWID()), @FK_TabLineCombinationID, @FK_PreparationMethodID, @PreparationMethodName);

    SELECT *
    FROM POS_TabLinePreparationMethods
    WHERE TabLinePreparationMethodID = 
    (
        SELECT TOP 1 TabLinePreparationMethodID
        FROM @Inserted
    );
END
GO