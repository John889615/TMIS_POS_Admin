-- =============================================================
-- Migration : FOH-to-Admin Schema Reconciliation
-- Date      : 2026-05-06
-- Step      : 10 of 10 - Guests (no POS_ prefix; shared table)
--
-- Guests is a shared table referenced by POS_AccountGuests, POS_TabLineGuests,
-- BookingGuests, POS_Arrivals, and any non-POS code paths. There was no
-- Tables/.sql file for it in the Admin repo, even though Guest_Base.cs has
-- the full 14-column shape. This script is CREATE-or-ADD-MISSING-COLUMN.
--
-- After running this script:
--   1. Drop regenerable CRUD SPs so the generator rebuilds them:
--        IF OBJECT_ID('dbo.Guests_insert', 'P') IS NOT NULL DROP PROCEDURE dbo.Guests_insert;
--        IF OBJECT_ID('dbo.Guests_update', 'P') IS NOT NULL DROP PROCEDURE dbo.Guests_update;
--   2. Run the code generator. Guest_Base.cs already matches; this is mostly
--      a defensive top-up for any deployment whose live DB is missing a column.
--   3. Schema reconciliation is COMPLETE - move to Spec 2 (sync redesign).
--
-- This script is idempotent - safe to re-run.
-- =============================================================
USE [TMIS_Development];
GO

-- ----- Create table if missing -------------------------------
IF OBJECT_ID(N'[dbo].[Guests]', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[Guests]
    (
        [GuestID]            INT          NOT NULL PRIMARY KEY,
        [Title]              VARCHAR(10)  NULL,
        [FirstName]          VARCHAR(50)  NOT NULL,
        [MiddleName]         VARCHAR(50)  NULL,
        [LastName]           VARCHAR(50)  NOT NULL,
        [DateOfBirth]        DATE         NULL,
        [Gender]             VARCHAR(20)  NULL,
        [Nationality]        VARCHAR(50)  NULL,
        [PreferredLanguage]  VARCHAR(20)  NULL,
        [SpecialRequests]    VARCHAR(MAX) NULL,
        [LoyaltyNumber]      VARCHAR(50)  NULL,
        [Notes]              VARCHAR(MAX) NULL,
        [DateCreated]        DATETIME     NULL CONSTRAINT [DF_Guests_DateCreated] DEFAULT GETDATE(),
        [DateUpdated]        DATETIME     NULL CONSTRAINT [DF_Guests_DateUpdated] DEFAULT GETDATE()
    );
END
GO

-- ----- ADD missing columns (if table existed but is older) ---
IF NOT EXISTS (SELECT 1 FROM sys.columns
               WHERE object_id = OBJECT_ID(N'[dbo].[Guests]') AND name = N'Title')
    ALTER TABLE [dbo].[Guests] ADD [Title] VARCHAR(10) NULL;
GO

IF NOT EXISTS (SELECT 1 FROM sys.columns
               WHERE object_id = OBJECT_ID(N'[dbo].[Guests]') AND name = N'FirstName')
    ALTER TABLE [dbo].[Guests] ADD [FirstName] VARCHAR(50) NOT NULL CONSTRAINT [DF_Guests_FirstName] DEFAULT '';
GO

IF NOT EXISTS (SELECT 1 FROM sys.columns
               WHERE object_id = OBJECT_ID(N'[dbo].[Guests]') AND name = N'MiddleName')
    ALTER TABLE [dbo].[Guests] ADD [MiddleName] VARCHAR(50) NULL;
GO

IF NOT EXISTS (SELECT 1 FROM sys.columns
               WHERE object_id = OBJECT_ID(N'[dbo].[Guests]') AND name = N'LastName')
    ALTER TABLE [dbo].[Guests] ADD [LastName] VARCHAR(50) NOT NULL CONSTRAINT [DF_Guests_LastName] DEFAULT '';
GO

IF NOT EXISTS (SELECT 1 FROM sys.columns
               WHERE object_id = OBJECT_ID(N'[dbo].[Guests]') AND name = N'DateOfBirth')
    ALTER TABLE [dbo].[Guests] ADD [DateOfBirth] DATE NULL;
GO

IF NOT EXISTS (SELECT 1 FROM sys.columns
               WHERE object_id = OBJECT_ID(N'[dbo].[Guests]') AND name = N'Gender')
    ALTER TABLE [dbo].[Guests] ADD [Gender] VARCHAR(20) NULL;
GO

IF NOT EXISTS (SELECT 1 FROM sys.columns
               WHERE object_id = OBJECT_ID(N'[dbo].[Guests]') AND name = N'Nationality')
    ALTER TABLE [dbo].[Guests] ADD [Nationality] VARCHAR(50) NULL;
GO

IF NOT EXISTS (SELECT 1 FROM sys.columns
               WHERE object_id = OBJECT_ID(N'[dbo].[Guests]') AND name = N'PreferredLanguage')
    ALTER TABLE [dbo].[Guests] ADD [PreferredLanguage] VARCHAR(20) NULL;
GO

IF NOT EXISTS (SELECT 1 FROM sys.columns
               WHERE object_id = OBJECT_ID(N'[dbo].[Guests]') AND name = N'SpecialRequests')
    ALTER TABLE [dbo].[Guests] ADD [SpecialRequests] VARCHAR(MAX) NULL;
GO

IF NOT EXISTS (SELECT 1 FROM sys.columns
               WHERE object_id = OBJECT_ID(N'[dbo].[Guests]') AND name = N'LoyaltyNumber')
    ALTER TABLE [dbo].[Guests] ADD [LoyaltyNumber] VARCHAR(50) NULL;
GO

IF NOT EXISTS (SELECT 1 FROM sys.columns
               WHERE object_id = OBJECT_ID(N'[dbo].[Guests]') AND name = N'Notes')
    ALTER TABLE [dbo].[Guests] ADD [Notes] VARCHAR(MAX) NULL;
GO

IF NOT EXISTS (SELECT 1 FROM sys.columns
               WHERE object_id = OBJECT_ID(N'[dbo].[Guests]') AND name = N'DateCreated')
    ALTER TABLE [dbo].[Guests] ADD [DateCreated] DATETIME NULL CONSTRAINT [DF_Guests_DateCreated] DEFAULT GETDATE();
GO

IF NOT EXISTS (SELECT 1 FROM sys.columns
               WHERE object_id = OBJECT_ID(N'[dbo].[Guests]') AND name = N'DateUpdated')
    ALTER TABLE [dbo].[Guests] ADD [DateUpdated] DATETIME NULL CONSTRAINT [DF_Guests_DateUpdated] DEFAULT GETDATE();
GO
