USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.EntityAddresses_select_single', 'P') IS NOT NULL
    DROP PROCEDURE dbo.EntityAddresses_select_single;
GO

CREATE PROCEDURE dbo.EntityAddresses_select_single
    @EntityAddressID INT
AS
BEGIN
    SELECT *
    FROM EntityAddresses
    WHERE EntityAddressID = @EntityAddressID;
END
GO