USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_Accounts_update', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_Accounts_update;
GO

CREATE PROCEDURE dbo.POS_Accounts_update
    @AccountID UNIQUEIDENTIFIER,
    @Name VARCHAR(50),
    @FK_BookingHeaderID INT,
    @IsClosed BIT,
    @FK_ResponsibleID INT,
    @DateCreated DATETIME,
    @DateUpdated DATETIME = NULL
AS
BEGIN
    UPDATE POS_Accounts
    SET     [Name] = @Name,
    FK_BookingHeaderID = @FK_BookingHeaderID,
    IsClosed = @IsClosed,
    FK_ResponsibleID = @FK_ResponsibleID,
    DateUpdated = @DateUpdated
    WHERE AccountID = @AccountID;

    SELECT *
    FROM POS_Accounts
    WHERE AccountID = @AccountID;
END
GO