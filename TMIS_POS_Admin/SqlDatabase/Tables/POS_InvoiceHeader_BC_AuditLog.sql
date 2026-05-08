USE [TMIS_Development]
GO

IF OBJECT_ID('POS_InvoiceHeader_BC_AuditLog', 'U') IS NOT NULL
    DROP TABLE POS_InvoiceHeader_BC_AuditLog
GO

CREATE TABLE POS_InvoiceHeader_BC_AuditLog
(
    InvoiceHeaderBcAuditLogID UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(),

    FK_InvoiceHeaderID UNIQUEIDENTIFIER NOT NULL
        FOREIGN KEY REFERENCES POS_InvoiceHeaders(InvoiceHeaderID),

    AttemptedAt DATETIME NOT NULL DEFAULT GETDATE(),

    -- Free-text pipeline stage. Values used by Bc_Push_Service:
    --   Validate | CreateOrder | AddLine | ShipAndInvoice | Resume | OrderOnly
    Stage   VARCHAR(50) NULL,

    -- 'Success' | 'Failure'
    Outcome VARCHAR(20) NOT NULL,

    BC_SalesOrderID VARCHAR(255) NULL,
    BC_SalesOrderNo VARCHAR(50)  NULL,
    BC_InvoiceID    VARCHAR(255) NULL,
    BC_InvoiceNo    VARCHAR(50)  NULL,

    ErrorMessage    VARCHAR(MAX) NULL
)
GO

CREATE INDEX IX_POS_InvoiceHeader_BC_AuditLog_FK_InvoiceHeaderID
    ON POS_InvoiceHeader_BC_AuditLog (FK_InvoiceHeaderID, AttemptedAt DESC)
GO

CREATE INDEX IX_POS_InvoiceHeader_BC_AuditLog_AttemptedAt
    ON POS_InvoiceHeader_BC_AuditLog (AttemptedAt DESC)
GO
