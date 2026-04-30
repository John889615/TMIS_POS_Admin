using System;

namespace POS_Common.Models.Stock.Custom.StockRequestSelectAllStockRequest
{
    public class Req_StockRequestSelectAllStockRequest
    {
        #region Properties

        /// <summary>
        /// Maps to @FK_ToDebtorID
        /// </summary>
        public int? FKToDebtorID { get; set; }

        /// <summary>
        /// Maps to @FK_FromDebtorID
        /// </summary>
        public int? FKFromDebtorID { get; set; }

        /// <summary>
        /// Maps to @FK_OrderStatusID
        /// </summary>
        public int? FKOrderStatusID { get; set; }

        #endregion
    }
}
