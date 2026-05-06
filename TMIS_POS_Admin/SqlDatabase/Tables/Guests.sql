USE [TMIS_Development]
GO

IF OBJECT_ID('Guests', 'U') IS NOT NULL
	DROP TABLE Guests
GO

CREATE TABLE Guests
(
    GuestID            INT          NOT NULL PRIMARY KEY,
    Title              VARCHAR(10)  NULL,
    FirstName          VARCHAR(50)  NOT NULL,
    MiddleName         VARCHAR(50)  NULL,
    LastName           VARCHAR(50)  NOT NULL,
    DateOfBirth        DATE         NULL,
    Gender             VARCHAR(20)  NULL,
    Nationality        VARCHAR(50)  NULL,
    PreferredLanguage  VARCHAR(20)  NULL,
    SpecialRequests    VARCHAR(MAX) NULL,
    LoyaltyNumber      VARCHAR(50)  NULL,
    Notes              VARCHAR(MAX) NULL,
    DateCreated        DATETIME     NULL DEFAULT GETDATE(),
    DateUpdated        DATETIME     NULL DEFAULT GETDATE()
)
