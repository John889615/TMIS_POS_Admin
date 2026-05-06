-- =============================================================
-- Migration : FOH-to-Admin Schema Reconciliation
-- Date      : 2026-05-06
-- Step      : 08 of 09 - BookingHeaders (no POS_ prefix; shared table)
--
-- BookingHeaders is a shared table referenced by POS_Accounts.FK_BookingHeaderID
-- and used outside of POS as well. There was no Tables/.sql file for it in the
-- Admin repo, even though POS_Accounts FKs to it. This script:
--   - Creates the table if it does not exist (matching FOH structure).
--   - Adds any missing columns if it does exist (idempotent).
--   - Adds the UNIQUE constraint on BookingReference if missing.
--
-- After running this script:
--   1. Drop regenerable CRUD SPs so the generator rebuilds them:
--        IF OBJECT_ID('dbo.BookingHeaders_insert', 'P') IS NOT NULL DROP PROCEDURE dbo.BookingHeaders_insert;
--        IF OBJECT_ID('dbo.BookingHeaders_update', 'P') IS NOT NULL DROP PROCEDURE dbo.BookingHeaders_update;
--   2. Run the code generator. It should regenerate:
--        - BookingHeader_Base.cs (currently empty - will pick up all 8 columns)
--        - Generated CRUD SPs.
--   3. Then move on to script 09 (BookingGuests, which FKs to BookingHeaders).
--
-- This script is idempotent - safe to re-run.
-- =============================================================
USE [TMIS_Development];
GO

-- ----- Create table if missing -------------------------------
IF OBJECT_ID(N'[dbo].[BookingHeaders]', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[BookingHeaders]
    (
        [BookingHeaderID]   INT          NOT NULL PRIMARY KEY,
        [PartyName]         VARCHAR(150) NOT NULL,
        [BookingReference]  VARCHAR(50)  NOT NULL,
        [TravelStart]       DATE         NULL,
        [TravelEnd]         DATE         NULL,
        [DateCreated]       DATETIME     NOT NULL CONSTRAINT [DF_BookingHeaders_DateCreated]    DEFAULT GETDATE(),
        [DateUpdated]       DATETIME     NULL,
        [IsStaffBooking]    BIT          NOT NULL CONSTRAINT [DF_BookingHeaders_IsStaffBooking] DEFAULT 0,

        CONSTRAINT [UQ_BookingHeaders_BookingReference] UNIQUE ([BookingReference])
    );
END
GO

-- ----- ADD missing columns (if table existed but is older) ---
IF NOT EXISTS (SELECT 1 FROM sys.columns
               WHERE object_id = OBJECT_ID(N'[dbo].[BookingHeaders]')
                 AND name = N'PartyName')
    ALTER TABLE [dbo].[BookingHeaders] ADD [PartyName] VARCHAR(150) NOT NULL CONSTRAINT [DF_BookingHeaders_PartyName] DEFAULT '';
GO

IF NOT EXISTS (SELECT 1 FROM sys.columns
               WHERE object_id = OBJECT_ID(N'[dbo].[BookingHeaders]')
                 AND name = N'BookingReference')
    ALTER TABLE [dbo].[BookingHeaders] ADD [BookingReference] VARCHAR(50) NOT NULL CONSTRAINT [DF_BookingHeaders_BookingReference] DEFAULT '';
GO

IF NOT EXISTS (SELECT 1 FROM sys.columns
               WHERE object_id = OBJECT_ID(N'[dbo].[BookingHeaders]')
                 AND name = N'TravelStart')
    ALTER TABLE [dbo].[BookingHeaders] ADD [TravelStart] DATE NULL;
GO

IF NOT EXISTS (SELECT 1 FROM sys.columns
               WHERE object_id = OBJECT_ID(N'[dbo].[BookingHeaders]')
                 AND name = N'TravelEnd')
    ALTER TABLE [dbo].[BookingHeaders] ADD [TravelEnd] DATE NULL;
GO

IF NOT EXISTS (SELECT 1 FROM sys.columns
               WHERE object_id = OBJECT_ID(N'[dbo].[BookingHeaders]')
                 AND name = N'DateCreated')
    ALTER TABLE [dbo].[BookingHeaders] ADD [DateCreated] DATETIME NOT NULL CONSTRAINT [DF_BookingHeaders_DateCreated] DEFAULT GETDATE();
GO

IF NOT EXISTS (SELECT 1 FROM sys.columns
               WHERE object_id = OBJECT_ID(N'[dbo].[BookingHeaders]')
                 AND name = N'DateUpdated')
    ALTER TABLE [dbo].[BookingHeaders] ADD [DateUpdated] DATETIME NULL;
GO

IF NOT EXISTS (SELECT 1 FROM sys.columns
               WHERE object_id = OBJECT_ID(N'[dbo].[BookingHeaders]')
                 AND name = N'IsStaffBooking')
    ALTER TABLE [dbo].[BookingHeaders] ADD [IsStaffBooking] BIT NOT NULL CONSTRAINT [DF_BookingHeaders_IsStaffBooking] DEFAULT 0;
GO

-- ----- Ensure UNIQUE on BookingReference ---------------------
IF NOT EXISTS (SELECT 1 FROM sys.indexes
               WHERE object_id = OBJECT_ID(N'[dbo].[BookingHeaders]')
                 AND name = N'UQ_BookingHeaders_BookingReference')
BEGIN
    ALTER TABLE [dbo].[BookingHeaders]
        ADD CONSTRAINT [UQ_BookingHeaders_BookingReference] UNIQUE ([BookingReference]);
END
GO
