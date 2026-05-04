USE [TMIS_Development]
GO

IF OBJECT_ID('dbo.Debtors_insert', 'P') IS NOT NULL
    DROP PROCEDURE dbo.Debtors_insert;
GO

CREATE PROCEDURE dbo.Debtors_insert
    @ShortCode VARCHAR(8),
    @Name VARCHAR(255),
    @FK_MasterDebtorID INT = NULL,
    @IsMasterDebtor BIT,
    @DateCreated DATETIME,
    @DateUpdated DATETIME,
    @BC_ID VARCHAR(255) = NULL
AS
BEGIN
    DECLARE @Inserted TABLE (DebtorID INT);

    INSERT INTO Debtors (ShortCode, [Name], FK_MasterDebtorID, IsMasterDebtor, DateCreated, DateUpdated, BC_ID)
    OUTPUT INSERTED.DebtorID INTO @Inserted
    VALUES (@ShortCode, @Name, @FK_MasterDebtorID, @IsMasterDebtor, @DateCreated, @DateUpdated, @BC_ID);

    SELECT *
    FROM Debtors
    WHERE DebtorID = 
    (
        SELECT TOP 1 DebtorID
        FROM @Inserted
    );
END
GO