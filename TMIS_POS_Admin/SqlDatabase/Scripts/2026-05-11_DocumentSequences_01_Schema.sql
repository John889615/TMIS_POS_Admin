-- =============================================================
-- Migration : Document Sequences (generic Ref Number generator)
-- Date      : 2026-05-11
-- Step      : 01 of 02 (Schema + seed. Run BEFORE Step 02.)
--
-- Creates a single table the API uses to mint reference numbers
-- like "SR00001" for Stock Requests, "PO00001" for Purchase Orders,
-- etc. Each document type gets its own row with prefix, pad length,
-- and an atomically-incrementing counter.
--
-- Idempotent.
-- =============================================================
USE [TMIS_Development];
GO

-- ----- POS_DocumentSequences --------------------------------
IF NOT EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[POS_DocumentNoSequences]') AND type = N'U')
BEGIN
    CREATE TABLE [dbo].POS_DocumentNoSequences
    (
        [DocumentSequenceID] INT             IDENTITY(1,1) NOT NULL,
        [DocumentType]       VARCHAR(50)     NOT NULL,
        [Prefix]             VARCHAR(10)     NOT NULL,
        [PadLength]          INT             NOT NULL CONSTRAINT [DF_POS_DocumentSequences_PadLength] DEFAULT (5),
        [NextNumber]         BIGINT          NOT NULL CONSTRAINT [DF_POS_DocumentSequences_NextNumber] DEFAULT (1),
        [DateCreated]        DATETIME        NOT NULL CONSTRAINT [DF_POS_DocumentSequences_DateCreated] DEFAULT (GETDATE()),
        [DateUpdated]        DATETIME        NOT NULL CONSTRAINT [DF_POS_DocumentSequences_DateUpdated] DEFAULT (GETDATE()),
        CONSTRAINT [PK_POS_DocumentSequences] PRIMARY KEY CLUSTERED ([DocumentSequenceID] ASC),
        CONSTRAINT [UQ_POS_DocumentSequences_DocumentType] UNIQUE ([DocumentType])
    );
END
GO

-- ----- Seed rows ---------------------------------------------
IF NOT EXISTS (SELECT 1 FROM [dbo].POS_DocumentNoSequences WHERE [DocumentType] = N'StockRequest')
BEGIN
    INSERT INTO [dbo].POS_DocumentNoSequences ([DocumentType], [Prefix], [PadLength], [NextNumber])
    VALUES (N'StockRequest', N'SR', 5, 1);
END
GO

-- Add additional document types here as they come online, e.g.:
-- IF NOT EXISTS (SELECT 1 FROM [dbo].[POS_DocumentSequences] WHERE [DocumentType] = N'PurchaseOrder')
-- BEGIN
--     INSERT INTO [dbo].[POS_DocumentSequences] ([DocumentType], [Prefix], [PadLength], [NextNumber])
--     VALUES (N'PurchaseOrder', N'PO', 5, 1);
-- END
-- GO
