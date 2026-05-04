USE [TMIS_Development]
GO

IF OBJECT_ID('dbo.Users_insert', 'P') IS NOT NULL
    DROP PROCEDURE dbo.Users_insert;
GO

CREATE PROCEDURE dbo.Users_insert
    @Firstname VARCHAR(255),
    @Lastname VARCHAR(255),
    @Username VARCHAR(255)
AS
BEGIN
    DECLARE @Inserted TABLE (UserID INT);

    INSERT INTO Users (Firstname, Lastname, Username)
    OUTPUT INSERTED.UserID INTO @Inserted
    VALUES (@Firstname, @Lastname, @Username);

    SELECT *
    FROM Users
    WHERE UserID = 
    (
        SELECT TOP 1 UserID
        FROM @Inserted
    );
END
GO