USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_TablineSubstitutes_update', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_TablineSubstitutes_update;
GO

CREATE PROCEDURE dbo.POS_TablineSubstitutes_update
    @TablineSubstituteID UNIQUEIDENTIFIER,
    @FK_ParentTabLineID UNIQUEIDENTIFIER,
    @FK_SubstituionTabLineID UNIQUEIDENTIFIER,
    @FK_ParentTabLineCombinationID UNIQUEIDENTIFIER = NULL
AS
BEGIN
    UPDATE POS_TablineSubstitutes
    SET     FK_ParentTabLineID = @FK_ParentTabLineID,
    FK_SubstituionTabLineID = @FK_SubstituionTabLineID,
    FK_ParentTabLineCombinationID = @FK_ParentTabLineCombinationID
    WHERE TablineSubstituteID = @TablineSubstituteID;

    SELECT *
    FROM POS_TablineSubstitutes
    WHERE TablineSubstituteID = @TablineSubstituteID;
END
GO