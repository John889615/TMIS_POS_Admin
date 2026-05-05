USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.Branches_select_single', 'P') IS NOT NULL
    DROP PROCEDURE dbo.Branches_select_single;
GO

CREATE PROCEDURE dbo.Branches_select_single
    @BranchID INT
AS
BEGIN
    SELECT *
    FROM Branches
    WHERE BranchID = @BranchID;
END
GO