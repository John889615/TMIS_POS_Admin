USE [TMIS_Development]
GO

IF OBJECT_ID('dbo.POS_Accounts_insert', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_Accounts_insert;
GO

CREATE PROCEDURE dbo.POS_Accounts_insert
    @Name VARCHAR(50),
    @FK_BookingHeaderID INT,
    @IsClosed BIT,
    @FK_ResponsibleID INT,
    @DateCreated DATETIME,
    @DateUpdated DATETIME = NULL
AS
BEGIN
    DECLARE @Inserted TABLE (AccountID UNIQUEIDENTIFIER);

    INSERT INTO POS_Accounts ([Name], FK_BookingHeaderID, IsClosed, FK_ResponsibleID, DateCreated, DateUpdated)
    OUTPUT INSERTED.AccountID INTO @Inserted
    VALUES (@Name, @FK_BookingHeaderID, @IsClosed, @FK_ResponsibleID, @DateCreated, @DateUpdated);

    SELECT *
    FROM POS_Accounts
    WHERE AccountID = 
    (
        SELECT TOP 1 AccountID
        FROM @Inserted
    );
END
GO