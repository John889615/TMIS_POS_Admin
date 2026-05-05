USE [TMIS_Development]
GO

IF OBJECT_ID('dbo.POS_TablineSubstitutes_insert', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_TablineSubstitutes_insert;
GO

CREATE PROCEDURE dbo.POS_TablineSubstitutes_insert
    @FK_ParentTabLineID UNIQUEIDENTIFIER,
    @FK_SubstituionTabLineID UNIQUEIDENTIFIER,
    @FK_ParentTabLineCombinationID UNIQUEIDENTIFIER = NULL
AS
BEGIN
    DECLARE @Inserted TABLE (POS_TablineSubstituteID UNIQUEIDENTIFIER);

    INSERT INTO POS_TablineSubstitutes (FK_ParentTabLineID, FK_SubstituionTabLineID, FK_ParentTabLineCombinationID)
    OUTPUT INSERTED.POS_TablineSubstituteID INTO @Inserted
    VALUES (@FK_ParentTabLineID, @FK_SubstituionTabLineID, @FK_ParentTabLineCombinationID);

    SELECT *
    FROM POS_TablineSubstitutes
    WHERE POS_TablineSubstituteID = 
    (
        SELECT TOP 1 POS_TablineSubstituteID
        FROM @Inserted
    );
END
GO