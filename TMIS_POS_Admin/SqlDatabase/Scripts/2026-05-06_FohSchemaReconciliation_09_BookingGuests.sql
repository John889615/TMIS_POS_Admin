-- =============================================================
-- Migration : FOH-to-Admin Schema Reconciliation
-- Date      : 2026-05-06
-- Step      : 09 of 09 - BookingGuests (no POS_ prefix; shared table)
--
-- BookingGuests is a shared table referenced by FOH BookingGuests sync.
-- BookingGuest_Base.cs already has matching properties on Admin, so the
-- table likely exists on the live DB but had no Tables/.sql file. This
-- script is CREATE-if-missing + ADD-if-missing-column for safety.
--
-- IMPORTANT: must be run AFTER step 08 (BookingHeaders) because of the
-- FK constraint.
--
-- After running this script:
--   1. Drop regenerable CRUD SPs so the generator rebuilds them:
--        IF OBJECT_ID('dbo.BookingGuests_insert', 'P') IS NOT NULL DROP PROCEDURE dbo.BookingGuests_insert;
--        IF OBJECT_ID('dbo.BookingGuests_update', 'P') IS NOT NULL DROP PROCEDURE dbo.BookingGuests_update;
--   2. Run the code generator.
--   3. Then move on to script 10 (Guests, the last shared table).
--   4. POS_Arrivals already matches FOH (varchar CheckedInBy/CheckedOutBy
--      shadow columns per staff-not-synced rule); no script needed.
--
-- This script is idempotent - safe to re-run.
-- =============================================================
USE [TMIS_Development];
GO

-- ----- Create table if missing -------------------------------
IF OBJECT_ID(N'[dbo].[BookingGuests]', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[BookingGuests]
    (
        [BookingGuestID]      INT      NOT NULL PRIMARY KEY,
        [FK_GuestID]          INT      NULL,
        [FK_BookingHeaderID]  INT      NOT NULL,
        [DateCreated]         DATETIME NOT NULL CONSTRAINT [DF_BookingGuests_DateCreated] DEFAULT GETDATE(),
        [DateUpdated]         DATETIME NULL
    );

    ALTER TABLE [dbo].[BookingGuests]
        ADD CONSTRAINT [FK_BookingGuests_Guest]
            FOREIGN KEY ([FK_GuestID]) REFERENCES [dbo].[Guests] ([GuestID]);

    ALTER TABLE [dbo].[BookingGuests]
        ADD CONSTRAINT [FK_BookingGuests_BookingHeader]
            FOREIGN KEY ([FK_BookingHeaderID]) REFERENCES [dbo].[BookingHeaders] ([BookingHeaderID]);
END
GO

-- ----- ADD missing columns (if table existed but is older) ---
IF NOT EXISTS (SELECT 1 FROM sys.columns
               WHERE object_id = OBJECT_ID(N'[dbo].[BookingGuests]')
                 AND name = N'FK_GuestID')
    ALTER TABLE [dbo].[BookingGuests] ADD [FK_GuestID] INT NULL;
GO

IF NOT EXISTS (SELECT 1 FROM sys.columns
               WHERE object_id = OBJECT_ID(N'[dbo].[BookingGuests]')
                 AND name = N'FK_BookingHeaderID')
    ALTER TABLE [dbo].[BookingGuests] ADD [FK_BookingHeaderID] INT NULL; -- added NULL; tighten manually if all rows backfilled
GO

IF NOT EXISTS (SELECT 1 FROM sys.columns
               WHERE object_id = OBJECT_ID(N'[dbo].[BookingGuests]')
                 AND name = N'DateCreated')
    ALTER TABLE [dbo].[BookingGuests] ADD [DateCreated] DATETIME NOT NULL CONSTRAINT [DF_BookingGuests_DateCreated] DEFAULT GETDATE();
GO

IF NOT EXISTS (SELECT 1 FROM sys.columns
               WHERE object_id = OBJECT_ID(N'[dbo].[BookingGuests]')
                 AND name = N'DateUpdated')
    ALTER TABLE [dbo].[BookingGuests] ADD [DateUpdated] DATETIME NULL;
GO

-- ----- Ensure FK constraints --------------------------------
IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys
               WHERE parent_object_id = OBJECT_ID(N'[dbo].[BookingGuests]')
                 AND name = N'FK_BookingGuests_Guest')
    ALTER TABLE [dbo].[BookingGuests]
        ADD CONSTRAINT [FK_BookingGuests_Guest]
            FOREIGN KEY ([FK_GuestID]) REFERENCES [dbo].[Guests] ([GuestID]);
GO

IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys
               WHERE parent_object_id = OBJECT_ID(N'[dbo].[BookingGuests]')
                 AND name = N'FK_BookingGuests_BookingHeader')
    ALTER TABLE [dbo].[BookingGuests]
        ADD CONSTRAINT [FK_BookingGuests_BookingHeader]
            FOREIGN KEY ([FK_BookingHeaderID]) REFERENCES [dbo].[BookingHeaders] ([BookingHeaderID]);
GO
