USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_PaymentTypeIcons_update', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_PaymentTypeIcons_update;
GO

CREATE PROCEDURE dbo.POS_PaymentTypeIcons_update
    @PaymentTypeIconID INT,
    @IconPath VARCHAR(50),
    @Category VARCHAR(255),
    @DateCreated DATETIME
AS
BEGIN
    UPDATE POS_PaymentTypeIcons
    SET     IconPath = @IconPath,
    Category = @Category
    WHERE PaymentTypeIconID = @PaymentTypeIconID;

    SELECT *
    FROM POS_PaymentTypeIcons
    WHERE PaymentTypeIconID = @PaymentTypeIconID;
END
GO