USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.EntityAddress_select_addressID', 'P') IS NOT NULL
    DROP PROCEDURE dbo.EntityAddress_select_addressID;
GO

CREATE PROCEDURE dbo.EntityAddress_select_addressID
	@FK_AddressID INT
AS
BEGIN
    SELECT *
	FROM EntityAddresses
	WHERE FK_AddressID = @FK_AddressID
END
GO