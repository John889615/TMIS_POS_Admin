using System;

namespace POS_Common.Models.Stock.Custom.StockRequestReviewersSelectByDebtorRole
{
    public class Res_StockRequestReviewersSelectByDebtorRole
    {
        #region Properties

        /// <summary>
        /// Maps to POS_StockRequestReviewerID
        /// </summary>
        public int? POSStockRequestReviewerID { get; set; }

        /// <summary>
        /// Maps to FK_ToDebtorID
        /// </summary>
        public int? FKToDebtorID { get; set; }

        /// <summary>
        /// Maps to FK_UserID
        /// </summary>
        public int? FKUserID { get; set; }

        /// <summary>
        /// Maps to Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Maps to DisplayName
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Maps to Role
        /// </summary>
        public string Role { get; set; }

        /// <summary>
        /// Maps to IsActive
        /// </summary>
        public bool? IsActive { get; set; }

        /// <summary>
        /// Maps to DateCreated
        /// </summary>
        public DateTime? DateCreated { get; set; }

        #endregion
    }
}
