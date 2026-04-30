-- =============================================================
-- Migration : Stock Request Approval workflow - schema additions
-- Date      : 2026-04-30
-- Step      : 01 of N (Schema only. Run BEFORE the code generator.)
--
-- After running this script:
--   1. Run the Code Generator. It should regenerate:
--      - StockRequest_Base.cs / StockRequestLine_Base.cs (new columns)
--      - StockRequestReviewer_Base.cs (new model)
--      - Stock_Base_Service.cs CRUD wrappers (new params)
--      - POS_StockRequests_insert / _update SPs (new columns)
--      - POS_StockRequestLines_insert / _update SPs (new column)
--      - POS_StockRequestReviewers_* CRUD SPs (new table)
--   2. Then I will deliver Script 02 (status seed + custom SPs).
--
-- This script is idempotent - safe to re-run.
-- =============================================================
USE [TMIS_Development];
GO

-- ----- 1. POS_StockRequests : approval audit columns --------
IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[POS_StockRequests]') AND name = N'FK_ApprovedByUserID')
BEGIN
    ALTER TABLE [dbo].[POS_StockRequests]
        ADD [FK_ApprovedByUserID] INT NULL;
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[POS_StockRequests]') AND name = N'DateApproved')
BEGIN
    ALTER TABLE [dbo].[POS_StockRequests]
        ADD [DateApproved] DATETIME NULL;
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = N'FK_POS_StockRequests_ApprovedByUser')
BEGIN
    ALTER TABLE [dbo].[POS_StockRequests] WITH CHECK
        ADD CONSTRAINT [FK_POS_StockRequests_ApprovedByUser]
        FOREIGN KEY ([FK_ApprovedByUserID]) REFERENCES [dbo].[Users] ([UserID]);
END
GO

-- ----- 2. POS_StockRequestLines : approved quantity ---------
-- NULL  => no decision yet
-- 0     => declined (also IsDeclined = 1)
-- < Quantity => partially approved
-- = Quantity => fully approved
IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[POS_StockRequestLines]') AND name = N'ApprovedQuantity')
BEGIN
    ALTER TABLE [dbo].[POS_StockRequestLines]
        ADD [ApprovedQuantity] DECIMAL(18,4) NULL;
END
GO

-- ----- 3. POS_StockRequestReviewers : email routing table ---
IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE object_id = OBJECT_ID(N'[dbo].[POS_StockRequestReviewers]'))
BEGIN
    CREATE TABLE [dbo].[POS_StockRequestReviewers] (
        [POS_StockRequestReviewerID] INT             IDENTITY(1,1) NOT NULL,
        [FK_ToDebtorID]              INT             NOT NULL,
        [FK_UserID]                  INT             NULL,
        [Email]                      NVARCHAR(256)   NOT NULL,
        [DisplayName]                NVARCHAR(128)   NULL,
        [Role]                       VARCHAR(20)     NOT NULL,
        [IsActive]                   BIT             NOT NULL CONSTRAINT [DF_POS_StockRequestReviewers_IsActive]    DEFAULT(1),
        [DateCreated]                DATETIME        NOT NULL CONSTRAINT [DF_POS_StockRequestReviewers_DateCreated] DEFAULT(GETDATE()),
        CONSTRAINT [PK_POS_StockRequestReviewers] PRIMARY KEY CLUSTERED ([POS_StockRequestReviewerID]),
        CONSTRAINT [FK_POS_StockRequestReviewers_ToDebtor] FOREIGN KEY ([FK_ToDebtorID]) REFERENCES [dbo].[Debtors] ([DebtorID]),
        CONSTRAINT [FK_POS_StockRequestReviewers_User]     FOREIGN KEY ([FK_UserID])     REFERENCES [dbo].[Users]   ([UserID]),
        CONSTRAINT [CK_POS_StockRequestReviewers_Role]     CHECK ([Role] IN ('Approver','Buyer'))
    );
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'IX_POS_StockRequestReviewers_DebtorRole' AND object_id = OBJECT_ID(N'[dbo].[POS_StockRequestReviewers]'))
BEGIN
    CREATE NONCLUSTERED INDEX [IX_POS_StockRequestReviewers_DebtorRole]
        ON [dbo].[POS_StockRequestReviewers] ([FK_ToDebtorID], [Role], [IsActive])
        INCLUDE ([FK_UserID], [Email], [DisplayName]);
END
GO
