USE [TMIS_Development]
GO

IF OBJECT_ID('dbo.POS_RequestFromServer', 'U') IS NOT NULL
    DROP TABLE dbo.POS_RequestFromServer
GO

CREATE TABLE dbo.POS_RequestFromServer
(
    RequestFromServerID INT IDENTITY(1,1) PRIMARY KEY,
    [Type] VARCHAR(50) NOT NULL,
    LastRequestDate DATETIME NULL,
    CallSequence INT NOT NULL CHECK (CallSequence >= 0),
    SyncFrequency INT NOT NULL CHECK (SyncFrequency > 0),
    IsActive BIT NOT NULL DEFAULT(1),
	ApiUrl VARCHAR(255) NOT NULL,
)

INSERT INTO POS_RequestFromServer ([Type], LastRequestDate, CallSequence, SyncFrequency, IsActive, ApiUrl)
VALUES ('InvoiceHeaders', null, 1, 50, 1, 'invoice/headers'),
('InvoiceLines', null, 2, 50, 1, 'invoice/lines')