USE [TMIS_BlueSafaris]
GO

IF OBJECT_ID('dbo.BulkUpsertToServer_BookingGuests', 'P') IS NOT NULL
    DROP PROCEDURE dbo.BulkUpsertToServer_BookingGuests;
GO

CREATE PROCEDURE dbo.BulkUpsertToServer_BookingGuests
    @Rows dbo.BulkInsertToServer READONLY
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    BEGIN TRY
        BEGIN TRAN;

        ;WITH Src AS
        (
            SELECT
                Int1 AS BookingGuestID,
                Int2 AS FK_BookingHeaderID,
                Int3 AS FK_GuestID,
                Date1 AS DateCreated,
                Date2 AS DateUpdated,
                CAST(String1 AS VARCHAR(50)) AS SyncStatus
            FROM @Rows
            WHERE Int1 IS NOT NULL
        )
        SELECT * INTO #Src FROM Src;

        DELETE bg
        FROM dbo.BookingGuests bg
        INNER JOIN #Src s
            ON s.BookingGuestID = bg.BookingGuestID
        WHERE s.SyncStatus = 'DELETE_PENDING';

        ;WITH UpsertSrc AS
        (
            SELECT BookingGuestID, FK_BookingHeaderID, FK_GuestID, DateCreated, DateUpdated
            FROM #Src
            WHERE ISNULL(SyncStatus, 'NOT_SYNCED') <> 'DELETE_PENDING'
        )
        MERGE dbo.BookingGuests AS T
        USING UpsertSrc AS S
          ON T.BookingGuestID = S.BookingGuestID
        WHEN MATCHED THEN
            UPDATE SET
                T.FK_BookingHeaderID = S.FK_BookingHeaderID,
                T.FK_GuestID = S.FK_GuestID,
                T.DateCreated = S.DateCreated,
                T.DateUpdated = S.DateUpdated
        WHEN NOT MATCHED BY TARGET THEN
            INSERT (BookingGuestID, FK_BookingHeaderID, FK_GuestID, DateCreated, DateUpdated)
            VALUES (S.BookingGuestID, S.FK_BookingHeaderID, S.FK_GuestID, S.DateCreated, S.DateUpdated);

        COMMIT TRAN;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRAN;
        THROW;
    END CATCH
END
GO