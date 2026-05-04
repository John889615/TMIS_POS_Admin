USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.Users_update', 'P') IS NOT NULL
    DROP PROCEDURE dbo.Users_update;
GO

CREATE PROCEDURE dbo.Users_update
    @UserID INT,
    @Firstname VARCHAR(255),
    @Lastname VARCHAR(255),
    @Username VARCHAR(255)
AS
BEGIN
    UPDATE Users
    SET     Firstname = @Firstname,
    Lastname = @Lastname,
    Username = @Username
    WHERE UserID = @UserID;

    SELECT *
    FROM Users
    WHERE UserID = @UserID;
END
GO