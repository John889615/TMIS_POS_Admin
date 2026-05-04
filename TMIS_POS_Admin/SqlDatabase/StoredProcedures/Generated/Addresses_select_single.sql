USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.Addresses_select_single', 'P') IS NOT NULL
    DROP PROCEDURE dbo.Addresses_select_single;
GO

CREATE PROCEDURE dbo.Addresses_select_single
    @AddressID INT
AS
BEGIN
    SELECT *
    FROM Addresses
    WHERE AddressID = @AddressID;
END
GO