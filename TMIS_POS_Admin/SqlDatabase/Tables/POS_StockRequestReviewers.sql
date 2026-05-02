USE [TMIS_Development]
GO

IF OBJECT_ID('POS_StockRequestReviewers', 'U') IS NOT NULL
	DROP TABLE POS_StockRequestReviewers
GO

CREATE TABLE POS_StockRequestReviewers (
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
GO

CREATE NONCLUSTERED INDEX [IX_POS_StockRequestReviewers_DebtorRole]
    ON [dbo].[POS_StockRequestReviewers] ([FK_ToDebtorID], [Role], [IsActive])
    INCLUDE ([FK_UserID], [Email], [DisplayName]);
GO
