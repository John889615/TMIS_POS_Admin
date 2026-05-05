USE [TMIS_Development]
GO

IF OBJECT_ID('dbo.POS_PaymentTypeIcons_insert', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_PaymentTypeIcons_insert;
GO

CREATE PROCEDURE dbo.POS_PaymentTypeIcons_insert
    @IconPath VARCHAR(50),
    @Category VARCHAR(255),
    @DateCreated DATETIME
AS
BEGIN
    DECLARE @Inserted TABLE (PaymentTypeIconID INT);

    INSERT INTO POS_PaymentTypeIcons (IconPath, Category, DateCreated)
    OUTPUT INSERTED.PaymentTypeIconID INTO @Inserted
    VALUES (@IconPath, @Category, @DateCreated);

    SELECT *
    FROM POS_PaymentTypeIcons
    WHERE PaymentTypeIconID = 
    (
        SELECT TOP 1 PaymentTypeIconID
        FROM @Inserted
    );
END
GO