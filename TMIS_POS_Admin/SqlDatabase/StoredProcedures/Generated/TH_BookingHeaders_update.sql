USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.TH_BookingHeaders_update', 'P') IS NOT NULL
    DROP PROCEDURE dbo.TH_BookingHeaders_update;
GO

CREATE PROCEDURE dbo.TH_BookingHeaders_update
    @BookingHeaderID INT,
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
    UPDATE TH_BookingHeaders
    SET     PartyName = @PartyName,
    BookingReference = @BookingReference,
    FK_AgentDebtorID = @FK_AgentDebtorID,
    FK_BranchID = @FK_BranchID,
    FK_DepartmentID = @FK_DepartmentID,
    FK_CurrencyID = @FK_CurrencyID,
    QuoteTotal = @QuoteTotal,
    BookingTotal = @BookingTotal,
    FK_BookingStatusID = @FK_BookingStatusID,
    TravelStart = @TravelStart,
    TravelEnd = @TravelEnd,
    FK_CreatedUserID = @FK_CreatedUserID,
    FK_UpdatedUserID = @FK_UpdatedUserID,
    DateUpdated = @DateUpdated
    WHERE BookingHeaderID = @BookingHeaderID;

    SELECT *
    FROM TH_BookingHeaders
    WHERE BookingHeaderID = @BookingHeaderID;
END
GO