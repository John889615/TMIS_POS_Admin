USE [TMIS_Development]
GO

IF OBJECT_ID('BatchDedupe', 'U') IS NOT NULL
	DROP TABLE BatchDedupe
GO

CREATE TABLE BatchDedupe
(
    BatchID      UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
    SiteID       INT              NOT NULL,
    GroupName    VARCHAR(20)      NOT NULL,
    ReceivedAt   DATETIME         NOT NULL DEFAULT GETDATE(),
    ResultJson   VARCHAR(MAX)     NOT NULL
)
GO

CREATE INDEX IX_BatchDedupe_ReceivedAt ON BatchDedupe (ReceivedAt)
GO

CREATE INDEX IX_BatchDedupe_Site_Group ON BatchDedupe (SiteID, GroupName, ReceivedAt)
GO
