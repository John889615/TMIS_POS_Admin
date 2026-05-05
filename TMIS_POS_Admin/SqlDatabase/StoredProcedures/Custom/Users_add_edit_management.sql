USE [TMIS_Development]
GO

IF OBJECT_ID('Users_add_edit_management', 'P') IS NOT NULL
    DROP PROCEDURE Users_add_edit_management;
GO

CREATE PROCEDURE Users_add_edit_management
	@UserID INT,
	@Firstname VARCHAR(255),
	@Lastname VARCHAR(255),
	@Username VARCHAR(255)
AS
BEGIN
    IF EXISTS (SELECT 1
			   FROM Users
			   WHERE UserID = @UserID)
	BEGIN
		UPDATE Users
		SET Firstname = @Firstname
			, Lastname = @Lastname
			, Username = @Username
		WHERE UserID = @UserID
	END
	ELSE
	BEGIN
		INSERT INTO Users (UserID, Firstname, Lastname, Username)
		VALUES (@UserID, @Firstname, @Lastname, @Username)
	END
END
GO