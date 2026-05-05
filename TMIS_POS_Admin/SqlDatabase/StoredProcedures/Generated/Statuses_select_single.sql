USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.Statuses_select_single', 'P') IS NOT NULL
    DROP PROCEDURE dbo.Statuses_select_single;
GO

CREATE PROCEDURE dbo.Statuses_select_single
    @StatusID INT
AS
BEGIN
    SELECT *
    FROM Statuses
    WHERE StatusID = @StatusID;
END
GO