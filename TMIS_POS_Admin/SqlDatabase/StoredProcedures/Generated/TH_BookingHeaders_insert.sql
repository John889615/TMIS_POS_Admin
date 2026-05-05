USE [TMIS_Development]
GO

IF OBJECT_ID('dbo.TH_BookingHeaders_insert', 'P') IS NOT NULL
    DROP PROCEDURE dbo.TH_BookingHeaders_insert;
GO

CREATE PROCEDURE dbo.TH_BookingHeaders_insert
    @PartyName VARCHAR(150),
    @BookingReference VARCHAR(50),
    @FK_AgentDebtorID INT = NULL,
    @FK_BranchID INT,
    @FK_DepartmentID INT,
    @FK_CurrencyID INT,
    @QuoteTotal DECIMAL (18, 4),
    @BookingTotal DECIMAL (18, 4),
    @FK_BookingStatusID INT,
    @TravelStart DATE = NULL,
    @TravelEnd DATE = NULL,
    @FK_CreatedUserID INT,
    @FK_UpdatedUserID INT = NULL,
    @DateCreated DATETIME,
    @DateUpdated DATETIME = NULL
AS
BEGIN
    DECLARE @Inserted TABLE (BookingHeaderID INT);

    INSERT INTO TH_BookingHeaders (PartyName, BookingReference, FK_AgentDebtorID, FK_BranchID, FK_DepartmentID, FK_CurrencyID, QuoteTotal, BookingTotal, FK_BookingStatusID, TravelStart, TravelEnd, FK_CreatedUserID, FK_UpdatedUserID, DateCreated, DateUpdated)
    OUTPUT INSERTED.BookingHeaderID INTO @Inserted
    VALUES (@PartyName, @BookingReference, @FK_AgentDebtorID, @FK_BranchID, @FK_DepartmentID, @FK_CurrencyID, @QuoteTotal, @BookingTotal, @FK_BookingStatusID, @TravelStart, @TravelEnd, @FK_CreatedUserID, @FK_UpdatedUserID, @DateCreated, @DateUpdated);

    SELECT *
    FROM TH_BookingHeaders
    WHERE BookingHeaderID = 
    (
        SELECT TOP 1 BookingHeaderID
        FROM @Inserted
    );
END
GO