USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.ContactTypes_select_single', 'P') IS NOT NULL
    DROP PROCEDURE dbo.ContactTypes_select_single;
GO

CREATE PROCEDURE dbo.ContactTypes_select_single
    @ContactTypeID INT
AS
BEGIN
    SELECT *
    FROM ContactTypes
    WHERE ContactTypeID = @ContactTypeID;
END
GO