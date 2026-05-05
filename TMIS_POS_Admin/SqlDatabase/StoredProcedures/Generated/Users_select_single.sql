USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.Users_select_single', 'P') IS NOT NULL
    DROP PROCEDURE dbo.Users_select_single;
GO

CREATE PROCEDURE dbo.Users_select_single
    @UserID INT
AS
BEGIN
    SELECT *
    FROM Users
    WHERE UserID = @UserID;
END
GO