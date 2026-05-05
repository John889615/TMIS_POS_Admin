USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.Debtors_update', 'P') IS NOT NULL
    DROP PROCEDURE dbo.Debtors_update;
GO

CREATE PROCEDURE dbo.Debtors_update
    @DebtorID INT,
    @ShortCode VARCHAR(8),
    @Name VARCHAR(255),
    @FK_MasterDebtorID INT = NULL,
    @IsMasterDebtor BIT,
    @DateCreated DATETIME,
    @DateUpdated DATETIME,
    @BC_ID VARCHAR(255) = NULL
AS
BEGIN
    UPDATE Debtors
    SET     ShortCode = @ShortCode,
    [Name] = @Name,
    FK_MasterDebtorID = @FK_MasterDebtorID,
    IsMasterDebtor = @IsMasterDebtor,
    DateUpdated = @DateUpdated,
    BC_ID = @BC_ID
    WHERE DebtorID = @DebtorID;

    SELECT *
    FROM Debtors
    WHERE DebtorID = @DebtorID;
END
GO