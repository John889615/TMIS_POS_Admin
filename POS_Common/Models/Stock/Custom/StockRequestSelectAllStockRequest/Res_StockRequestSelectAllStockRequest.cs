using System;

namespace POS_Common.Models.Stock.Custom.StockRequestSelectAllStockRequest
{
    public class Res_StockRequestSelectAllStockRequest
    {
        #region Properties

        /// <summary>
        /// Maps to StockRequestID
        /// </summary>
        public int? StockRequestID { get; set; }

        /// <summary>
        /// Maps to RefNumber
        /// </summary>
        public string RefNumber { get; set; }

        /// <summary>
        /// Maps to FK_FromDebtorID
        /// </summary>
        public int? FKFromDebtorID { get; set; }

        /// <summary>
        /// Maps to FromDebtorName
        /// </summary>
        public string FromDebtorName { get; set; }

        /// <summary>
        /// Maps to FK_ToDebtorID
        /// </summary>
        public int? FKToDebtorID { get; set; }

        /// <summary>
        /// Maps to ToDebtorName
        /// </summary>
        public string ToDebtorName { get; set; }

        /// <summary>
        /// Maps to FK_OrderStatusID
        /// </summary>
        public int? FKOrderStatusID { get; set; }

        /// <summary>
        /// Maps to OrderStatus
        /// </summary>
        public string OrderStatus { get; set; }

        /// <summary>
        /// Maps to FK_UserID
        /// </summary>
        public int? FKUserID { get; set; }

        /// <summary>
        /// Maps to CreatedBy
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Maps to ManagerNotes
        /// </summary>
        public string ManagerNotes { get; set; }

        /// <summary>
        /// Maps to Notes
        /// </summary>
        public string Notes { get; set; }

        /// <summary>
        /// Maps to DateOrdered
        /// </summary>
        public DateTime? DateOrdered { get; set; }

        /// <summary>
        /// Maps to DateUpdated
        /// </summary>
        public DateTime? DateUpdated { get; set; }

        /// <summary>
        /// Maps to FK_ApprovedByUserID
        /// </summary>
        public int? FKApprovedByUserID { get; set; }

        /// <summary>
        /// Maps to DateApproved
        /// </summary>
        public DateTime? DateApproved { get; set; }

        #endregion
    }
}
