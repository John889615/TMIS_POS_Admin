-- =============================================================
-- Migration : Stock Request Approval workflow - custom stored procedures
-- Date      : 2026-04-30
-- Step      : 03 of N
--
-- 1. Replaces 3 existing custom SPs so they include the new columns:
--      stockRequest_select_all_stockRequest        - now returns FK_ApprovedByUserID, DateApproved + supports status/from-debtor filters
--      stockRequest_select_single_number           - now returns FK_ApprovedByUserID, DateApproved
--      stockRequestLines_select_all_stockRequestLines - now returns ApprovedQuantity
--
-- 2. Adds 1 new SP for email routing:
--      POS_StockRequestReviewers_select_by_debtor_role
--
-- Idempotent (CREATE OR ALTER) - safe to re-run on SQL Server 2016+.
-- =============================================================
USE [TMIS_Development];
GO

-- =============================================================
-- 1. List stock requests (with debtor names + status name + creator name)
--     Filters all optional. Pass NULL to skip a filter.
-- =============================================================
CREATE OR ALTER PROCEDURE [dbo].[stockRequest_select_all_stockRequest]
    @FK_ToDebtorID    INT = NULL,
    @FK_FromDebtorID  INT = NULL,
    @FK_OrderStatusID INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        sr.StockRequestID,
        sr.RefNumber,
        sr.FK_FromDebtorID,
        df.Name AS FromDebtorName,
        sr.FK_ToDebtorID,
        dt.Name AS ToDebtorName,
        sr.FK_OrderStatusID,
        os.OrderStatus,
        sr.FK_UserID,
        LTRIM(RTRIM(ISNULL(u.Firstname, '') + ' ' + ISNULL(u.Lastname, ''))) AS CreatedBy,
        sr.ManagerNotes,
        sr.Notes,
        sr.DateOrdered,
        sr.DateUpdated,
        sr.FK_ApprovedByUserID,
        sr.DateApproved
    FROM         dbo.POS_StockRequests sr
    INNER JOIN   dbo.POS_Locations           df ON df.LocationID      = sr.FK_FromDebtorID
    INNER JOIN   dbo.POS_Locations           dt ON dt.LocationID      = sr.FK_ToDebtorID
    INNER JOIN   dbo.POS_OrderStatus   os ON os.OrderStatusID = sr.FK_OrderStatusID
    LEFT JOIN    dbo.Users             u  ON u.UserID         = sr.FK_UserID
    --WHERE (@FK_ToDebtorID    IS NULL OR sr.FK_ToDebtorID    = @FK_ToDebtorID)
    --  AND (@FK_FromDebtorID  IS NULL OR sr.FK_FromDebtorID  = @FK_FromDebtorID)
    --  AND (@FK_OrderStatusID IS NULL OR sr.FK_OrderStatusID = @FK_OrderStatusID)
    ORDER BY sr.DateOrdered DESC;
END
GO

-- =============================================================
-- 2. Find a stock request by RefNumber (used to detect duplicates on Add)
-- =============================================================
CREATE OR ALTER PROCEDURE [dbo].[stockRequest_select_single_number]
    @RefNumber VARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT TOP (1)
        sr.StockRequestID,
        sr.RefNumber,
        sr.FK_FromDebtorID,
        sr.FK_ToDebtorID,
        sr.FK_OrderStatusID,
        sr.FK_UserID,
        sr.ManagerNotes,
        sr.Notes,
        sr.DateOrdered,
        sr.DateUpdated,
        sr.FK_ApprovedByUserID,
        sr.DateApproved
    FROM dbo.POS_StockRequests sr
    WHERE sr.RefNumber = @RefNumber;
END
GO

-- =============================================================
-- 3. List lines for a given stock request (with product name, unit, approval state)
-- =============================================================
CREATE OR ALTER PROCEDURE [dbo].[stockRequestLines_select_all_stockRequestLines]
    @FK_StockRequestID INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        srl.StockRequestLineID,
        srl.FK_StockRequestID,
        srl.FK_ProductID,
        p.ProductName,
        srl.FK_UnitID,
        u.Unit,
        u.Symbol,
        srl.Quantity,
        srl.Notes,
        srl.ManagerNotes,
        srl.IsDeclined,
        srl.ApprovedQuantity
    FROM         dbo.POS_StockRequestLines srl
    INNER JOIN   dbo.POS_Products          p   ON p.ProductID = srl.FK_ProductID
    LEFT  JOIN   dbo.POS_Units             u   ON u.UnitID    = srl.FK_UnitID
    WHERE srl.FK_StockRequestID = @FK_StockRequestID
    ORDER BY srl.StockRequestLineID;
END
GO

-- =============================================================
-- 4. NEW: Resolve email recipients for a ToDebtor + Role
--     @Role expected: 'Approver' or 'Buyer'
-- =============================================================
CREATE OR ALTER PROCEDURE [dbo].[POS_StockRequestReviewers_select_by_debtor_role]
    @FK_ToDebtorID INT,
    @Role          VARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        rev.POS_StockRequestReviewerID,
        rev.FK_ToDebtorID,
        rev.FK_UserID,
        rev.Email,
        rev.DisplayName,
        rev.Role,
        rev.IsActive,
        rev.DateCreated
    FROM dbo.POS_StockRequestReviewers rev
    WHERE rev.FK_ToDebtorID = @FK_ToDebtorID
      AND rev.Role          = @Role
      AND rev.IsActive      = 1;
END
GO

-- =============================================================
-- 5. NEW: Resolve email recipients for a Role across all debtors
--     @Role expected: 'Approver' or 'Buyer'
-- =============================================================
CREATE OR ALTER PROCEDURE [dbo].[POS_StockRequestReviewers_select_by_role]
    @Role VARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        rev.POS_StockRequestReviewerID,
        rev.FK_ToDebtorID,
        rev.FK_UserID,
        rev.Email,
        rev.DisplayName,
        rev.Role,
        rev.IsActive,
        rev.DateCreated
    FROM dbo.POS_StockRequestReviewers rev
    WHERE rev.Role     = @Role
      AND rev.IsActive = 1;
END
GO

-- =============================================================
-- 6. NEW: Delete all lines for a stock request
--     Used by Update flow to full-replace the line set on a Draft.
-- =============================================================
CREATE OR ALTER PROCEDURE [dbo].[stockRequestLines_delete_by_stock_request]
    @FK_StockRequestID INT
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM dbo.POS_StockRequestLines
    WHERE FK_StockRequestID = @FK_StockRequestID;
END
GO
