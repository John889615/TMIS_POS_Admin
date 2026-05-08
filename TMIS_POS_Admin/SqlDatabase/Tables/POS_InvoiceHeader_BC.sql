USE [TMIS_Development]
GO

IF OBJECT_ID('POS_InvoiceHeader_BC', 'U') IS NOT NULL
    DROP TABLE POS_InvoiceHeader_BC
GO

CREATE TABLE POS_InvoiceHeader_BC
(
    InvoiceHeaderBcID  UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(),

    FK_InvoiceHeaderID UNIQUEIDENTIFIER NOT NULL UNIQUE
        FOREIGN KEY REFERENCES POS_InvoiceHeaders(InvoiceHeaderID),

    BC_InvoiceID     VARCHAR(255) NULL,
    BC_InvoiceNo     VARCHAR(50)  NULL,
    BC_SalesOrderID  VARCHAR(255) NULL,
    BC_SalesOrderNo  VARCHAR(50)  NULL,

    BC_PushedAt      DATETIME     NULL,
    BC_LastError     VARCHAR(MAX) NULL,
    BC_LastAttemptAt DATETIME     NULL
)
GO

CREATE INDEX IX_POS_InvoiceHeader_BC_BCInvoiceID
    ON POS_InvoiceHeader_BC (BC_InvoiceID)
    WHERE BC_InvoiceID IS NOT NULL
GO

CREATE INDEX IX_POS_InvoiceHeader_BC_LastAttemptAt
    ON POS_InvoiceHeader_BC (BC_LastAttemptAt)
GO
