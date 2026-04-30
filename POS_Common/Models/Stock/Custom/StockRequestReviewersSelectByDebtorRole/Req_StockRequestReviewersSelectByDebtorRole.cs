using System;

namespace POS_Common.Models.Stock.Custom.StockRequestReviewersSelectByDebtorRole
{
    public class Req_StockRequestReviewersSelectByDebtorRole
    {
        #region Properties

        /// <summary>
        /// Maps to @FK_ToDebtorID
        /// </summary>
        public int? FKToDebtorID { get; set; }

        /// <summary>
        /// Maps to @Role
        /// </summary>
        public string Role { get; set; }

        #endregion
    }
}
