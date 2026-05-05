USE [TMIS_BlueSafaris]
GO

IF OBJECT_ID('dbo.BulkUpsertToServer_AccountGuests', 'P') IS NOT NULL
    DROP PROCEDURE dbo.BulkUpsertToServer_AccountGuests;
GO

CREATE PROCEDURE dbo.BulkUpsertToServer_AccountGuests
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
                Guid1 AS AccountGuestID,
                Guid2 AS FK_AccountID,
                Int1 AS FK_GuestID,
                Bool1 AS IsResponsible,
                Date1 AS DateCreated,
                Date2 AS DateUpdated,
                CAST(String1 AS VARCHAR(50)) AS SyncStatus
            FROM @Rows
            WHERE Guid1 IS NOT NULL
        )
        SELECT * INTO #Src FROM Src;

        DELETE ag
        FROM dbo.POS_AccountGuests ag
        INNER JOIN #Src s
            ON s.AccountGuestID = ag.AccountGuestID
        WHERE s.SyncStatus = 'DELETE_PENDING';

        ;WITH UpsertSrc AS
        (
            SELECT AccountGuestID, FK_AccountID, FK_GuestID, IsResponsible, DateCreated, DateUpdated
            FROM #Src
            WHERE ISNULL(SyncStatus, 'NOT_SYNCED') <> 'DELETE_PENDING'
        )
        MERGE dbo.POS_AccountGuests AS T
        USING UpsertSrc AS S
          ON T.AccountGuestID = S.AccountGuestID
        WHEN MATCHED THEN
            UPDATE SET
                T.FK_AccountID = S.FK_AccountID,
                T.FK_GuestID = S.FK_GuestID,
                T.IsResponsible = S.IsResponsible,
                T.DateCreated = S.DateCreated,
                T.DateUpdated = S.DateUpdated
        WHEN NOT MATCHED BY TARGET THEN
            INSERT (AccountGuestID, FK_AccountID, FK_GuestID, IsResponsible, DateCreated, DateUpdated)
            VALUES (S.AccountGuestID, S.FK_AccountID, S.FK_GuestID, S.IsResponsible, S.DateCreated, S.DateUpdated);

        COMMIT TRAN;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRAN;
        THROW;
    END CATCH
END
GO