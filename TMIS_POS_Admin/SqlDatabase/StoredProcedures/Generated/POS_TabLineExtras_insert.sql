USE [TMIS_Development]
GO

IF OBJECT_ID('dbo.POS_TabLineExtras_insert', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_TabLineExtras_insert;
GO

CREATE PROCEDURE dbo.POS_TabLineExtras_insert
    @FK_TabLineID UNIQUEIDENTIFIER,
    @FK_ProductID INT,
    @Product VARCHAR(255)
AS
BEGIN
    DECLARE @Inserted TABLE (TabLineExtraID UNIQUEIDENTIFIER);

    INSERT INTO POS_TabLineExtras (FK_TabLineID, FK_ProductID, Product)
    OUTPUT INSERTED.TabLineExtraID INTO @Inserted
    VALUES (@FK_TabLineID, @FK_ProductID, @Product);

    SELECT *
    FROM POS_TabLineExtras
    WHERE TabLineExtraID = 
    (
        SELECT TOP 1 TabLineExtraID
        FROM @Inserted
    );
END
GO