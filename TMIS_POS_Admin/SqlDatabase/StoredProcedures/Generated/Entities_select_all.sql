USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.Entities_select_all', 'P') IS NOT NULL
    DROP PROCEDURE dbo.Entities_select_all;
GO

CREATE PROCEDURE dbo.Entities_select_all
AS
BEGIN
    SELECT *
    FROM Entities;
END
GO