USE [TMIS_Development]
GO

IF OBJECT_ID('dbo.POS_TablineSubstitutes_insert', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_TablineSubstitutes_insert;
GO

CREATE PROCEDURE dbo.POS_TablineSubstitutes_insert
    @TablineSubstituteID UNIQUEIDENTIFIER = NULL,
    @FK_ParentTabLineID UNIQUEIDENTIFIER,
    @FK_SubstituionTabLineID UNIQUEIDENTIFIER,
    @FK_ParentTabLineCombinationID UNIQUEIDENTIFIER = NULL
AS
BEGIN
    DECLARE @Inserted TABLE (TablineSubstituteID UNIQUEIDENTIFIER);

    INSERT INTO POS_TablineSubstitutes (TablineSubstituteID, FK_ParentTabLineID, FK_SubstituionTabLineID, FK_ParentTabLineCombinationID)
    OUTPUT INSERTED.TablineSubstituteID INTO @Inserted
    VALUES (ISNULL(@TablineSubstituteID, NEWID()), @FK_ParentTabLineID, @FK_SubstituionTabLineID, @FK_ParentTabLineCombinationID);

    SELECT *
    FROM POS_TablineSubstitutes
    WHERE TablineSubstituteID = 
    (
        SELECT TOP 1 TablineSubstituteID
        FROM @Inserted
    );
END
GO