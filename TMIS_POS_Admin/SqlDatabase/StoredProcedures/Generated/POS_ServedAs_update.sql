USE [TMIS_Development]
GO


IF OBJECT_ID('dbo.POS_ServedAs_update', 'P') IS NOT NULL
    DROP PROCEDURE dbo.POS_ServedAs_update;
GO

CREATE PROCEDURE dbo.POS_ServedAs_update
    @ServedAsID INT,
    @ServedAsType VARCHAR(20),
    @Name VARCHAR(50),
    @FK_CreatedUserID INT,
    @FK_UpdatedUserID INT = NULL,
    @DateCreated DATETIME,
    @DateUpdated DATETIME = NULL
AS
BEGIN
    UPDATE POS_ServedAs
    SET     ServedAsType = @ServedAsType,
    [Name] = @Name,
    FK_CreatedUserID = @FK_CreatedUserID,
    FK_UpdatedUserID = @FK_UpdatedUserID,
    DateUpdated = @DateUpdated
    WHERE ServedAsID = @ServedAsID;

    SELECT *
    FROM POS_ServedAs
    WHERE ServedAsID = @ServedAsID;
END
GO