USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.AddressTypes_select_single', 'P') IS NOT NULL
    DROP PROCEDURE dbo.AddressTypes_select_single;
GO

CREATE PROCEDURE dbo.AddressTypes_select_single
    @AddressTypeID INT
AS
BEGIN
    SELECT *
    FROM AddressTypes
    WHERE AddressTypeID = @AddressTypeID;
END
GO