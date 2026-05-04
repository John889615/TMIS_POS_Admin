USE [TMIS_BlueSafaris]
GO

IF OBJECT_ID('dbo.BulkUpsertToServer_Arrivals', 'P') IS NOT NULL
    DROP PROCEDURE dbo.BulkUpsertToServer_Arrivals;
GO

CREATE PROCEDURE dbo.BulkUpsertToServer_Arrivals
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
                Guid1 AS ArrivalID,
                Int1 AS FK_GuestID,
                String2 AS CheckedInBy,
                Date1 AS CheckInDate,
                String3 AS CheckedOutBy,
                Date2 AS CheckOutDate,
                CAST(String4 AS VARCHAR(50)) AS SyncStatus
            FROM @Rows
            WHERE Guid1 IS NOT NULL
        )
        SELECT * INTO #Src FROM Src;

        DELETE ar
        FROM dbo.POS_Arrivals ar
        INNER JOIN #Src s
            ON s.ArrivalID = ar.ArrivalID
        WHERE s.SyncStatus = 'DELETE_PENDING';

        ;WITH UpsertSrc AS
        (
            SELECT ArrivalID, FK_GuestID, CheckedInBy, CheckInDate, CheckedOutBy, CheckOutDate
            FROM #Src
            WHERE ISNULL(SyncStatus, 'NOT_SYNCED') <> 'DELETE_PENDING'
        )
        MERGE dbo.POS_Arrivals AS T
        USING UpsertSrc AS S
          ON T.ArrivalID = S.ArrivalID
        WHEN MATCHED THEN
            UPDATE SET
                T.FK_GuestID = S.FK_GuestID,
                T.CheckedInBy = S.CheckedInBy,
                T.CheckInDate = S.CheckInDate,
                T.CheckedOutBy = S.CheckedOutBy,
                T.CheckOutDate = S.CheckOutDate
        WHEN NOT MATCHED BY TARGET THEN
            INSERT (ArrivalID, FK_GuestID, CheckedInBy, CheckInDate, CheckedOutBy, CheckOutDate)
            VALUES (S.ArrivalID, S.FK_GuestID, S.CheckedInBy, S.CheckInDate, S.CheckedOutBy, S.CheckOutDate);

        COMMIT TRAN;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRAN;
        THROW;
    END CATCH
END
GO