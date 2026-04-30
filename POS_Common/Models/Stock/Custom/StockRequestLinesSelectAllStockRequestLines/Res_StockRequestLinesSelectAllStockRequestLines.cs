using System;

namespace POS_Common.Models.Stock.Custom.StockRequestLinesSelectAllStockRequestLines
{
    public class Res_StockRequestLinesSelectAllStockRequestLines
    {
        #region Properties

        /// <summary>
        /// Maps to StockRequestLineID
        /// </summary>
        public int? StockRequestLineID { get; set; }

        /// <summary>
        /// Maps to FK_StockRequestID
        /// </summary>
        public int? FKStockRequestID { get; set; }

        /// <summary>
        /// Maps to FK_ProductID
        /// </summary>
        public int? FKProductID { get; set; }

        /// <summary>
        /// Maps to ProductName
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// Maps to Quantity
        /// </summary>
        public decimal? Quantity { get; set; }

        /// <summary>
        /// Maps to Notes
        /// </summary>
        public string Notes { get; set; }

        /// <summary>
        /// Maps to ManagerNotes
        /// </summary>
        public string ManagerNotes { get; set; }

        /// <summary>
        /// Maps to IsDeclined
        /// </summary>
        public bool? IsDeclined { get; set; }

        /// <summary>
        /// Maps to ApprovedQuantity
        /// </summary>
        public decimal? ApprovedQuantity { get; set; }

        #endregion
    }
}
